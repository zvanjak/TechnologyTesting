using System;

namespace NHibernateTestProject.Database
{
    using System.Data.SqlClient;
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Oracle.ManagedDataAccess.Client;
    using global::NHibernate.Cfg;

    //instalirat nugetom odp.net!!! //treba instalirati i posebno ODTwithODAC na kompjuter)
    public class OracleDatabaseInfo:IDatabaseInfo
    {
        private const string Extension = ".dbf";
        public string ServerIp { get; set; }
        public string ServerPort { get; set; }
        public string ServiceName { get; set; }
        public string ServiceUsername { get; set; }
        public string ServicePassword { get; set; }
        public string DatabasePath { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string DatabaseName
        {
            get { return string.IsNullOrEmpty(DatabasePath) ? string.Empty : Path.GetFileNameWithoutExtension(DatabasePath); }
        }

        protected virtual string DatabaseDefaultDirectoryPath
        {
            get
            {
                return @"C:\database";
            }
        }

        public string DatabasePathExt
        {
            get
            {
                var databasePath = !DatabasePath.EndsWith(Extension) ? DatabasePath + Extension : DatabasePath;
                if (!databasePath.Contains(@"\"))
                    databasePath = Path.Combine(DatabaseDefaultDirectoryPath, databasePath); //dodaj defaultni folder ako je upisano samo ime
                return databasePath;
            }
        }

        public void CreateDatabase()
        {
            string connectionString = CreateConnectionString(ServerIp, ServerPort, ServiceName, ServiceUsername,
                                                              ServicePassword, true);
            using (var conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                if (DatabaseExists())
                {
                    throw new Exception("Database already exists.");
                }

                cmd.CommandText = string.Format(
                    "CREATE TABLESPACE {0} DATAFILE '{1}' SIZE 10M AUTOEXTEND ON NEXT 1M", DatabaseName,
                    DatabasePathExt);
                cmd.ExecuteNonQuery();

                if (!UserExists(Username))
                {
                    throw new Exception(string.Format("User {0} already exists", Username));
                }

                cmd.CommandText =
                    string.Format(
                        "CREATE USER {0} IDENTIFIED BY {1} DEFAULT TABLESPACE {2} TEMPORARY TABLESPACE TEMP",
                        Username, Password, DatabaseName);
                cmd.ExecuteNonQuery();
                cmd.CommandText = string.Format("GRANT CONNECT TO {0}", Username);
                cmd.ExecuteNonQuery();
                cmd.CommandText = string.Format("ALTER USER {0} QUOTA UNLIMITED ON {1}", Username,
                                                DatabaseName);
                cmd.ExecuteNonQuery();
                cmd.CommandText = string.Format("GRANT CREATE TABLE TO {0}", Username);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private bool UserExists(string username)
        {
            string connectionString = CreateConnectionString(ServerIp, ServerPort, ServiceName, ServiceUsername,
                                                             ServicePassword, true);
            using (var conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText = string.Format(
                    "SELECT COUNT(*) FROM  all_users WHERE username='{0}' OR username='{1}'",
                    username, username.ToUpper());
                object count = cmd.ExecuteScalar();
                return (decimal)count > 0;
            }
        }

        public void DropDatabase()
        {
            throw new NotImplementedException();
        }

        public bool DatabaseExists()
        {
            string connectionString = CreateConnectionString(ServerIp, ServerPort, ServiceName, ServiceUsername,
                                                             ServicePassword, true);
            using (var conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleCommand cmd = conn.CreateCommand();
                cmd.CommandText =
                    string.Format(
                        "SELECT COUNT(*) FROM  dba_tablespaces WHERE tablespace_name='{0}' OR tablespace_name='{1}'",
                        DatabaseName, DatabaseName.ToUpper());
                object count = cmd.ExecuteScalar();
                conn.Close();
                return (decimal)count > 0;
            }
        }

        public FluentConfiguration GetFluentConfiguration()
        {
            return Fluently.Configure()
                           .Database(MsSqlConfiguration.MsSql2008.ConnectionString(GetConnectionString()));
        }

        private string GetConnectionString()
        {
            return CreateConnectionString(ServerIp, ServerPort, ServiceName, Username, Password);
        }

        private static string CreateConnectionString(string serverIp, string serperPort, string serviceName,
                                                     string username, string password, bool isSysDba = false)
        {
            string baseString =
                string.Format(
                    "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={2})));",
                    serverIp, serperPort, serviceName);
            string loginString = string.Format("User Id={0};Password={1};", username, password);
            string userTypeString = isSysDba ? "DBA Privilege=SYSDBA;" : String.Empty;
            return baseString + loginString + userTypeString;
        }
    }
}
