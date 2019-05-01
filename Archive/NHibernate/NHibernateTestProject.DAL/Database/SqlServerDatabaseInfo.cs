using System;

namespace NHibernateTestProject.Database
{
    using System.Data.SqlClient;
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using Mapping;
    using global::NHibernate.Cfg;
    using global::NHibernate.Tool.hbm2ddl;
//    using Account = Model.Account;

    public class SqlServerDatabaseInfo : IDatabaseInfo
    {
        private const string LogFileName = "_log.ldf";
        private const string Extension = ".mdf";
        public string ServerName { get; set; }
        public string Username { get; set; }

        private string LogPath
        {
            get
            {
                return string.IsNullOrEmpty(DatabasePathExt)
                           ? string.Empty
                           : string.Format(@"{0}\{1}", Path.GetDirectoryName(DatabasePathExt), DatabaseName + LogFileName);
            }
        }

        public string DatabasePath { get; set; }
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
            var tmpConn = new SqlConnection
            {
                ConnectionString = CreateConnectionString(ServerName, "master", Username, Password)
            };
            string logName = Path.GetFileNameWithoutExtension(LogPath);

            string sqlCreateQuery =
                string.Format(
                       "CREATE DATABASE {0} ON PRIMARY (NAME = {0}, FILENAME = '{1}', SIZE=3MB, FILEGROWTH =10%) LOG ON (NAME= {2}, FILENAME= '{3}', SIZE=1MB, FILEGROWTH=10%)",
                    DatabaseName, DatabasePathExt, logName, LogPath);

            using (tmpConn)
            {
                using (var sqlCmd = new SqlCommand(sqlCreateQuery, tmpConn))
                {
                    tmpConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    tmpConn.Close();
                }
            }
        }

        public void DropDatabase()
        {
            var tmpConn = new SqlConnection
            {
                ConnectionString = CreateConnectionString(ServerName, "master", Username, Password)
            };
            string sqlCreateQuery = string.Format("DROP DATABASE [{0}]", DatabaseName);

            using (tmpConn)
            {
                using (var sqlCmd = new SqlCommand(sqlCreateQuery, tmpConn))
                {
                    tmpConn.Open();
                    sqlCmd.ExecuteNonQuery();
                    tmpConn.Close();
                }
            }
        }

        public bool DatabaseExists()
        {
            var tmpConn = new SqlConnection
            {
                ConnectionString = CreateConnectionString(ServerName, "master", Username, Password)
            };

            string sqlQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", DatabaseName);

            using (tmpConn)
            {
                using (var sqlCmd = new SqlCommand(sqlQuery, tmpConn))
                {
                    tmpConn.Open();
                    object databaseId = sqlCmd.ExecuteScalar();
                    tmpConn.Close();

                    return (databaseId != null);
                }
            }
        }

        public FluentConfiguration GetFluentConfiguration()
        {
            return Fluently.Configure()
                           .Database(MsSqlConfiguration.MsSql2008.ConnectionString(GetConnectionString()));
        }

        private string GetConnectionString()
        {
            return CreateConnectionString(ServerName, DatabaseName, Username, Password);
        }

        private static string CreateConnectionString(string serverName, string databaseName, string username = "",
                                                     string password = "")
        {
            string baseString = string.Format("Server={0};Database={1};", serverName, databaseName);
            string loginString = null;
            loginString += String.IsNullOrEmpty(username) && String.IsNullOrEmpty(password)
                               ? "Integrated Security=SSPI;"
                               : string.Format("User Id={0};Password={1};", username, password);
            const string marsString = "MultipleActiveResultSets=true;";
            //http://sradack.blogspot.com/2007/07/enable-mars-on-your-nhibernate.html
            return baseString + loginString + marsString;
        }
    }
}
