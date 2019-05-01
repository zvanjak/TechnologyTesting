using System;

namespace NHibernateTestProject.Database
{
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using MySql.Data.MySqlClient;
    using global::NHibernate.Cfg;

    //instalirat nugetom MySql.Data!!!
    public class MySqlDatabaseInfo : IDatabaseInfo
    {
        private const string Extension = ""; //ovdje je drugačije i nema baza ekstenziju već svaka tablica ima svoj file i on ima ekstenziju, ali to ovdje nije bitno
        public string ServerIp { get; set; }
        public string ServerPort { get; set; }
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
            string connectionString = CreateConnectionString(ServerIp, ServerPort, Username, Password);
            using (var conn = new MySqlConnection(connectionString))
            {
                string sqlCreateQuery =
                    string.Format(
                        "CREATE SCHEMA {0}", DatabaseName);

                using (var sqlCmd = new MySqlCommand(sqlCreateQuery, conn))
                {
                    conn.Open();
                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void DropDatabase()
        {
            string connectionString = CreateConnectionString(ServerIp, ServerPort, Username, Password);
            using (var conn = new MySqlConnection(connectionString))
            {
                string sqlCreateQuery =
                    string.Format(
                        "DROP SCHEMA {0}", DatabaseName);

                using (var sqlCmd = new MySqlCommand(sqlCreateQuery, conn))
                {
                    conn.Open();
                    sqlCmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public bool DatabaseExists()
        {
            string connectionString = GetConnectionString();
            using (var conn = new MySqlConnection(connectionString))
            {
                string sqlQuery =
                    string.Format("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{0}'",
                                  DatabaseName);

                using (var sqlCmd = new MySqlCommand(sqlQuery, conn))
                {
                    conn.Open();
                    object schemaName = sqlCmd.ExecuteScalar();
                    conn.Close();
                    return (schemaName != null);
                }
            }
        }

        public FluentConfiguration GetFluentConfiguration()
        {
            return Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(GetConnectionString()));
        }

        private string GetConnectionString()
        {
            return CreateConnectionString(ServerIp, ServerPort, Username, Password, DatabaseName);
        }

        private static string CreateConnectionString(string serverIp, string serperPort, string username,
                                                     string password, string databaseName = "")
        {
            string baseString = string.Format("Server={0}; Port={1};", serverIp, serperPort);
            string databaseString = !String.IsNullOrEmpty(databaseName)
                                        ? string.Format("Database={0};", databaseName)
                                        : string.Empty;
            string loginString = string.Format("Uid={0}; Pwd={1};", username, password);
            return baseString + databaseString + loginString;
        }
    }
}
