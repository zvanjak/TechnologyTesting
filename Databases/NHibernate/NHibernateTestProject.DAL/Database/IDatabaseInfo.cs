
namespace NHibernateTestProject.Database
{
    using FluentNHibernate.Cfg;

    public interface IDatabaseInfo
    {
        void CreateDatabase();
        void DropDatabase();
        bool DatabaseExists();
        FluentConfiguration GetFluentConfiguration();
    }
}
