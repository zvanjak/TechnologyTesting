﻿using System;

namespace NHibernateTestProject.Database
{
    using System.Data.SQLite;
    using System.Data.SqlServerCe;
    using System.IO;
    using FluentNHibernate.Cfg;
    using FluentNHibernate.Cfg.Db;
    using global::NHibernate.Cfg;

    //instalirat nugetom System.Data.Sqlite!!!
    public class SqliteDatabaseInfo:IDatabaseInfo
    {
        private const string Extension = ".db";
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
            if (File.Exists(DatabasePathExt))
            {
                File.Delete(DatabasePathExt);
            }
            SQLiteConnection.CreateFile(DatabasePathExt);
        }

        public void DropDatabase()
        {
            if (File.Exists(DatabasePathExt))
            {
                File.Delete(DatabasePathExt);
            }
        }

        public bool DatabaseExists()
        {
            return File.Exists(DatabasePathExt);
        }

        public FluentConfiguration GetFluentConfiguration()
        {
            return Fluently.Configure().Database(SQLiteConfiguration.Standard.ConnectionString(GetConnectionString()));
        }

       private string GetConnectionString()
        {
            string baseString = string.Format("Data Source={0};Version=3;BinaryGuid=False;FullColumnNames=true", DatabasePathExt);
            string loginString = String.IsNullOrEmpty(Password)
                                     ? string.Empty
                                     : string.Format("Password={0};", Password);
            return baseString + loginString;
        }
    }
}
