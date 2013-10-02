using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace tSQLt.Helpers
{
    public enum SqlOnOffOptions
    {
        On,
        Off
    }

    public class tSQLtTestUtils
    {

        public static void SetClr(SqlOnOffOptions onOffOption, string connectionString)
        {
            var sql = "EXEC sp_configure 'clr enabled', 1; " + Environment.NewLine +
                      "RECONFIGURE; ";

            ExecuteSql(sql, connectionString);
        }

        public static void SetTrustworthy(SqlOnOffOptions onOffOption, string connectionString)
        {
            var sql = @"DECLARE @cmd NVARCHAR(MAX); " + Environment.NewLine +
                      "SET @cmd='ALTER DATABASE ' + QUOTENAME(DB_NAME()) + ' SET TRUSTWORTHY ON;'; " +
                      Environment.NewLine +
                      "EXEC(@cmd); ";

            ExecuteSql(sql, connectionString);
        }

        public static void RepairAuthorization(string connectionString)
        {
            var sql = @"DECLARE @cmd NVARCHAR(MAX); " + Environment.NewLine +
                      "SET @cmd='ALTER AUTHORIZATION ON Database::' + QUOTENAME(DB_NAME()) + ' TO [sa]'; " +
                      Environment.NewLine +
                      "EXEC(@cmd); ";

            ExecuteSql(sql, connectionString);
        }

        public static void InstallTSQLT(string connectionString)
        {

            var resourcesHelper = new ResourcesHelper();
            var sql = resourcesHelper.GetEmbeddedResource("tSQLt.tSQLtInstallationScript", "tSQLt.class.sql");
            ExecuteNonQuerySMO(sql, connectionString);
        }

        public static void ApplyEmbeddedResourceScripts(IEnumerable<string> embeddedResourceScripts, string connectionString)
        {
            if (embeddedResourceScripts == null) return;
            foreach (var fullyQualifiedScriptName in embeddedResourceScripts)
            {
                ExecuteNonQuerySMO(fullyQualifiedScriptName, connectionString);
            }
        }

        private static void ExecuteNonQuerySMO(string sql, string connectionString)
        {
            var sqlConnection = new SqlConnection(connectionString);
            var svrConnection = new ServerConnection(sqlConnection);
            var server = new Server(svrConnection);
            server.ConnectionContext.ExecuteNonQuery(sql);
        }

        private static void ExecuteSql(string sql, string connectionString)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static IEnumerable<TestResult> GetTestResult(string name, string connectionString)
        {
            var sql = string.Format("select id, class, testcase, name, tranname, result, msg from tSQLt.TestResult where name='{0}'", name);
            var tests = new List<TestResult>();
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tests.Add(new TestResult
                                (
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Class")),
                                reader.GetString(reader.GetOrdinal("TestCase")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetString(reader.GetOrdinal("TranName")),
                                reader.GetString(reader.GetOrdinal("Result")),
                                reader.GetString(reader.GetOrdinal("Msg"))
                                ));
                        }
                    }
                }
            }

            return tests;
        }
    }

}