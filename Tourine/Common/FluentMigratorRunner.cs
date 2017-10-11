using System;
using System.Data.SqlClient;
using System.Reflection;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;

namespace Tourine.Common
{
    public class FluentMigratorRunner
    {
        public string ApplicationContext { get; set; }

        private string ConnectionString { get; }

        public SqlConnectionStringBuilder ConnectionBuilder { get; }

        public string DatabaseName { get; }

        public FluentMigratorRunner(string connectionString)
        {
            ConnectionString = connectionString;
            ConnectionBuilder = new SqlConnectionStringBuilder(connectionString);
            DatabaseName = ConnectionBuilder.InitialCatalog;
        }

        public SqlConnection CreateServerConnection()
        {
            var connectionBuilder = new SqlConnectionStringBuilder(ConnectionString);
            connectionBuilder.Remove("Initial Catalog");
            return new SqlConnection(connectionBuilder.ConnectionString);
        }

        private void DisableAutoCloseDatabaseOption()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var autoCloseEnabled = (int)new SqlCommand($"SELECT DATABASEPROPERTY('{connection.Database}','IsAutoClose')", connection).ExecuteScalar() == 1;
                if (autoCloseEnabled) new SqlCommand($"ALTER DATABASE [{connection.Database}] SET auto_close OFF WITH NO_WAIT", connection).ExecuteNonQuery();
            }

        }

        public void Migrate(Assembly migrationsAssembly)
        {
            if (!Db.Exists(ConnectionString))
                Db.Create(ConnectionString);
            DisableAutoCloseDatabaseOption();
            var log = "";
            try
            {
                var runnerContext = new RunnerContext(new TextWriterAnnouncer(x => log += x))
                {
                    Database = "SqlServer",
                    Timeout = 6000,
                    Connection = ConnectionString,
                    Targets = new[] { migrationsAssembly.Location },
                    Task = "migrate",
                    ApplicationContext = ApplicationContext
                };

                new TaskExecutor(runnerContext).Execute();
            }
            catch (Exception e)
            {
                throw new Exception($"Running db migrations has been failed: {log}", e);
            }
        }

    }

    public static class Db
    {

        public static int Execute(string connectionString, string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var c = new SqlCommand(sql, connection)) return c.ExecuteNonQuery();
            }
        }

        public static int? ExecuteScalar(string connectionString, string sql)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                object result;
                using (var c = new SqlCommand(sql, connection)) result = c.ExecuteScalar();
                return result == null || result == DBNull.Value ? null : (int?)result;
            }
        }

        public static void Create(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            using (var connection = new SqlConnection(connectionStringBuilder.WithInitialCatalog("").ToString()))
            {
                connection.Open();
                using (var command = new SqlCommand($"CREATE DATABASE [{connectionStringBuilder.InitialCatalog}]", connection)) command.ExecuteNonQuery();
            }
        }

        public static bool Exists(string connectionString)
        {
            var connectionStringBuilder = CreateConnectionStringBuilder(connectionString);
            using (var con = new SqlConnection(connectionStringBuilder.WithInitialCatalog("").ToString()))
            {
                con.Open();
                using (var command = new SqlCommand($"SELECT Count(*) FROM sysdatabases WHERE NAME= '{connectionStringBuilder.InitialCatalog}'", con))
                    if ((int)command.ExecuteScalar() > 0) return true;
            }
            return false;
        }

        public static bool HasTable(string connectionString, string table)
        {
            using (var con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (var command = new SqlCommand($"SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{table}'", con))
                        return (int)command.ExecuteScalar() > 0;
                }
                catch { return false; }
            }
        }

        public static bool DropIfExists(string connectionString)
        {
            if (!Exists(connectionString)) return false;
            Drop(connectionString);
            return true;
        }

        public static void Drop(string connectionString)
        {
            var connectionStringBuilder = CreateConnectionStringBuilder(connectionString);
            using (var connection = new SqlConnection(connectionStringBuilder.WithInitialCatalog("").ToString()))
            {
                connection.Open();
                var dbName = connectionStringBuilder.InitialCatalog;
                using (var command = new SqlCommand(@"
                            ALTER DATABASE [" + dbName + @"] set offline with rollback immediate;
                            ALTER DATABASE [" + dbName + @"] set online with rollback immediate;
                            DROP DATABASE [" + dbName + @"]", connection)) command.ExecuteNonQuery();
            }
        }

        private static SqlConnectionStringBuilder CreateConnectionStringBuilder(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            if (string.IsNullOrEmpty(connectionStringBuilder.InitialCatalog))
                throw new ArgumentException("Connection string should have initial catalog.", nameof(connectionString));
            return connectionStringBuilder;
        }

        public static SqlConnectionStringBuilder WithInitialCatalog(this SqlConnectionStringBuilder connectionStringBuilder, string initialCatalog)
        {
            return new SqlConnectionStringBuilder(connectionStringBuilder.ToString()) { InitialCatalog = initialCatalog };
        }
    }
}