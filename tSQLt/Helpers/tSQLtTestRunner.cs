using System;
using System.Data.SqlClient;
using System.Linq;
using NUnit.Framework;

namespace tSQLt.Helpers
{
    public class tSQLtTestRunner
    {
        public string TestFullName { get; private set; }
        private string _connectionString;

        public tSQLtTestRunner(string testFullName, string connectionString)
        {
            if (testFullName == null) throw new ArgumentNullException("testFullName");
            TestFullName = testFullName;
            _connectionString = connectionString;
        }

        public void Run()
        {
            ExecuteSqlTest();
            AssertOnResult();
        }

        private void AssertOnResult()
        {
            var testResult = tSQLtTestUtils.GetTestResult(TestFullName, _connectionString).FirstOrDefault();

            if (testResult == null)
                Assert.Fail("Could not find test");
            if (testResult.Result.ToLower() != "success")
                Assert.Fail(testResult.Msg);
        }

        private void ExecuteSqlTest()
        {
            var sql = string.Format("exec tSQLt.Run '{0}'", TestFullName);
            try
            {
                SqlHelper.ExecuteNonQuery(sql, _connectionString);
            }
            /* Leave this here to suppress errors in Nunit output */
            catch (Exception)
            {
            }
        }
    }

}