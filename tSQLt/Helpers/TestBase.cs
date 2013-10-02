using System;
using System.Collections.Generic;

namespace tSQLt.Helpers
{
    public class TestBase
    {
        public static void SetupTestFixture()
        {
            BootstrapForTests.BootstrapTestsDatabase();
        }

        public static void SetupTest()
        {
        }

        public static void RunTest(string fullTestName)
        {
            var localTestDatabaseConnectionString = BootstrapForTests.GetLocalTestDatabaseConnectionString();
            var testRunner = new tSQLtTestRunner(fullTestName, localTestDatabaseConnectionString);
            testRunner.Run();
        }

        // ReSharper disable UnusedMember.Global
        // This is used by the templates
        public static List<tSQLtTest> GetTests()
        // ReSharper restore UnusedMember.Global
        {
            try
            {
                var localTestDatabaseConnectionString = BootstrapForTests.GetLocalTestDatabaseConnectionString();
                return TemplateUtils.GetTests(localTestDatabaseConnectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new List<tSQLtTest>();
            }
        }
    }
}