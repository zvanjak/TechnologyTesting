using System.Windows;

namespace NHibernateTestProject
{
    using System;
    using Database;
    using NHibernate;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateSqlServerDatabase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var sqlServerDatabaseInfo = new SqlServerDatabaseInfo
                {
                    ServerName = sqlServer_serverName.Text,
                    DatabasePath = sqlServer_databasePath.Text,
                    Username = sqlServer_username.Text,
                    Password = sqlServer_password.Text
                };
                //sqlServerDatabaseInfo.CreateDatabase();
                CreateSchema(sqlServerDatabaseInfo);
                OpenSession(sqlServerDatabaseInfo);
                MessageBox.Show(string.Format("Database {0} is created", sqlServer_databasePath.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CreateSqlServerCEDatabase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var sqlServerCEDatabaseInfo = new SqlServerCEDatabaseInfo
                {
                    DatabasePath = sqlServerCE_databasePath.Text,
                    Password = sqlServerCE_password.Text
                };
                sqlServerCEDatabaseInfo.CreateDatabase();
                CreateSchema(sqlServerCEDatabaseInfo);
                OpenSession(sqlServerCEDatabaseInfo);
                MessageBox.Show(string.Format("Database {0} is created", sqlServer_databasePath.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CreateSqliteDatabase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var sqliteDatabaseInfo = new SqliteDatabaseInfo()
                {
                    DatabasePath = sqlite_databasePath.Text,
                    Password = sqlite_password.Text
                };
                sqliteDatabaseInfo.CreateDatabase();
                CreateSchema(sqliteDatabaseInfo);
                OpenSession(sqliteDatabaseInfo);
                MessageBox.Show(string.Format("Database {0} is created", sqlServer_databasePath.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CreateMySqlDatabase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var mySqlDatabaseInfo = new MySqlDatabaseInfo
                {
                    ServerIp = mySql_serverAddress.Text,
                    ServerPort = mySql_serverPort.Text,
                    DatabasePath = mySql_databaseName.Text,
                    Username = mySql_username.Text,
                    Password = mySql_password.Text
                };
                mySqlDatabaseInfo.CreateDatabase();
                CreateSchema(mySqlDatabaseInfo);
                OpenSession(mySqlDatabaseInfo);
                MessageBox.Show(string.Format("Database {0} is created", sqlServer_databasePath.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CreateOracleDatabase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var oracleDatabaseInfo = new OracleDatabaseInfo
                    {
                        ServerIp = oracle_serverAddress.Text,
                        ServerPort = oracle_serverPort.Text,
                        DatabasePath = oracle_databaseName.Text,
                        ServiceName = oracle_serviceName.Text,
                        ServiceUsername = oracle_serviceUsername.Text,
                        ServicePassword = oracle_servicepassword.Text,
                        Username = oracle_username.Text,
                        Password = oracle_password.Text
                    };
                oracleDatabaseInfo.CreateDatabase();
                CreateSchema(oracleDatabaseInfo);
                OpenSession(oracleDatabaseInfo);
                MessageBox.Show(string.Format("Database {0} is created", sqlServer_databasePath.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CreateSchema(IDatabaseInfo databaseInfo)
        {
            var nhService = new NhibernateService(databaseInfo);
            nhService.CreateDatabaseAndSchema();
        }

        private void OpenSession(IDatabaseInfo databaseInfo)
        {
            var configuration = databaseInfo.GetFluentConfiguration();
            var sessionFactory = configuration.BuildSessionFactory();
            var session = sessionFactory.OpenSession(); 
        }
    }
}
