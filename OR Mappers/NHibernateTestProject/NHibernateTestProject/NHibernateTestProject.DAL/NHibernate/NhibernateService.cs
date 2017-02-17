using System;

namespace NHibernateTestProject.NHibernate
{
    using Database;
    using Mapping;
    using global::NHibernate;
    using global::NHibernate.Cfg;
    using global::NHibernate.Tool.hbm2ddl;

    public class NhibernateService
    {
        private static ISessionFactory _sessionFactory;
        public IDatabaseInfo DatabaseInfo { private get; set; }

        public NhibernateService(IDatabaseInfo databaseInfo)
        {
            DatabaseInfo = databaseInfo;
        }

        public ISession OpenSession()
        {
            try
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = OpenSessionFactory();
                }
                ISession session = _sessionFactory.OpenSession();
                return session;
            }
            catch (Exception e)
            {
                throw e.InnerException ?? e;
            }
        }

        private ISessionFactory OpenSessionFactory()
        {
            if (DatabaseInfo == null)
            {
                return null;
            }
            var  configuration = DatabaseInfo.GetFluentConfiguration();
            return configuration.BuildSessionFactory();
        }

        public void CreateDatabaseAndSchema()
        {
            _sessionFactory = null; //obriše se eventualni prošli session
            if (DatabaseInfo == null)
            {
                return;
            }
            DatabaseInfo.CreateDatabase();
            CreateSchema();
        }

        private void CreateSchema()
        {
            var configuration = DatabaseInfo.GetFluentConfiguration();
            configuration.Mappings(m => m.FluentMappings.AddFromAssemblyOf<AccountMapping>()).
                          ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)).
                          BuildSessionFactory();
        }
    }
}
