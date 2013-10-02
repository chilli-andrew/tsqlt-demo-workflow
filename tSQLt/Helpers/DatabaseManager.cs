using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using tSQLt.Helpers;

namespace tSQLt
{
    public class DatabaseManager
    {
        private readonly string _connectionString;

        public DatabaseManager(string connectionString)
        {
            if (connectionString == null) throw new ArgumentNullException("connectionString");
            _connectionString = connectionString;
        }

        public string DatabaseName {
            get { return GetLocalTestDatabaseName(); }
        }

        public bool DatabaseExists()
        {

            return DatabaseExists(this.DatabaseName);
        }

        private bool DatabaseExists(string databaseName)
        {
            var result = (int)ExecuteScalar(string.Format("SELECT COUNT(name) FROM master.dbo.sysdatabases WHERE name='{0}'", databaseName));
            return result > 0;
        }

        private object ExecuteScalar(string sql)
        {
            var localMasterConnectionString = GetLocalMasterConnectionString();
            return SqlHelper.ExecuteScalar(sql, localMasterConnectionString);
        }

        public void CreateDatabase()
        {
            ExecuteNonQuery(string.Format("CREATE DATABASE {0}", DatabaseName));
        }

        public void DropDatabase(bool closeConnections = false)
        {
            if (!DatabaseExists(DatabaseName)) return;

            ExecuteNonQuery(string.Format(
                "EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'{0}'", DatabaseName));
            
            if (closeConnections)
                ExecuteNonQuery(string.Format(
                    "ALTER DATABASE [{0}] SET  SINGLE_USER WITH ROLLBACK IMMEDIATE", DatabaseName));

            ExecuteNonQuery(string.Format("DROP DATABASE [{0}]", DatabaseName));
        }

        private object ExecuteNonQuery(string sql)
        {
            var localMasterConnectionString = GetLocalMasterConnectionString();
            return SqlHelper.ExecuteNonQuery(sql, localMasterConnectionString);
        }



        private string GetLocalTestDatabaseName()
        {
            return GetLocalTestDatabaseSqlConnectionStringBuilder().InitialCatalog;
        }

        private string GetLocalMasterConnectionString()
        {
            var configBuilder = GetLocalTestDatabaseSqlConnectionStringBuilder();
            configBuilder.InitialCatalog = "master";
            return configBuilder.ConnectionString;
        }

        private SqlConnectionStringBuilder GetLocalTestDatabaseSqlConnectionStringBuilder()
        {
            return new SqlConnectionStringBuilder(_connectionString);
        }
    }
}
