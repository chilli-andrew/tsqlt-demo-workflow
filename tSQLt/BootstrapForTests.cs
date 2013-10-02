using System.Collections.Generic;
using System.Linq;
using tSQLt.Helpers;

namespace tSQLt
{
    public class BootstrapForTests
    {
        private static bool _alreadyBootstrapped = false;
        private static object _lock = new object();
        private static ResourcesHelper _resourcesHelper = new ResourcesHelper();
        public static void BootstrapTestsDatabase()
        {
            lock (_lock)
            {
                if (_alreadyBootstrapped) return;
                SetupTestDatabase();
                PrepareForTSQLT();
                InstallTSQLT();
                ApplyPatches();
                ApplyExistingTests();
                ApplyWorkInProgress();
                _alreadyBootstrapped = true;
            }

        }

        public static void ApplyWorkInProgress()
        {
            var resourceScripts = GetResourceScriptsForWorkInProgress();
            tSQLtTestUtils.ApplyEmbeddedResourceScripts(resourceScripts, GetLocalTestDatabaseConnectionString());
        }

        private static IEnumerable<string> GetResourceScriptsForWorkInProgress()
        {
            var embeddedResources = _resourcesHelper.GetAllEmbeddedResourcesInAssembly();
            var workInProgressResourceNames = embeddedResources.Where(s => s.StartsWith("tSQLt.WorkInProgress")).OrderBy(s => s).ToList();
            var resourceScripts = _resourcesHelper.GetEmbeddedResourceScriptsFrom(workInProgressResourceNames);
            return resourceScripts;
        }

        private static void ApplyExistingTests()
        {
            var resourceScripts = GetResourceScriptsForTests();
            tSQLtTestUtils.ApplyEmbeddedResourceScripts(resourceScripts, GetLocalTestDatabaseConnectionString());
        }

        private static IEnumerable<string> GetResourceScriptsForTests()
        {
            var embeddedResources = _resourcesHelper.GetAllEmbeddedResourcesInAssembly();
            var testResourceNames = embeddedResources.Where(s => s.StartsWith("tSQLt.Tests"));
            var resourceScripts = _resourcesHelper.GetEmbeddedResourceScriptsFrom(testResourceNames);
            return resourceScripts;
        }

        private static void InstallTSQLT()
        {
            tSQLtTestUtils.InstallTSQLT(GetLocalTestDatabaseConnectionString());
        }

        private static void ApplyPatches()
        {
            // here is where you would add any patches that need to be applied after your base database schema has been created.
            // we currently dp this using FluentMigrator (https://github.com/schambers/fluentmigrator)
        }

        private static void SetupTestDatabase()
        {
            var testDatabaseManager = CreateTestDatabaseManager();
            EnsureTestDatabaseExists(testDatabaseManager);
        }

        private static DatabaseManager CreateTestDatabaseManager()
        {
            var localTestDatabaseConnectionString = GetLocalTestDatabaseConnectionString();
            var testDatabaseManager = new DatabaseManager(localTestDatabaseConnectionString);
            return testDatabaseManager;
        }

        private static void EnsureTestDatabaseExists(DatabaseManager testDatabaseManager)
        {
            if (!testDatabaseManager.DatabaseExists())
            {
                // the code here could be changed to execute a sql script which represents a snapshot of the database at a point in time
                // the sql script could be an embedded resource and could be executed in a similar fashion to the tSQLt installation
                testDatabaseManager.CreateDatabase();
            }
        }

        private static void PrepareForTSQLT()
        {
            var localTestDatabaseConnectionString = GetLocalTestDatabaseConnectionString();
            tSQLtTestUtils.SetClr(SqlOnOffOptions.On, localTestDatabaseConnectionString);
            tSQLtTestUtils.SetTrustworthy(SqlOnOffOptions.On, localTestDatabaseConnectionString);
            tSQLtTestUtils.RepairAuthorization(localTestDatabaseConnectionString);
        }

        public static string GetLocalTestDatabaseConnectionString()
        {
            return "Data Source=localhost; Initial Catalog=AgeCalculator_Test; User ID=sa; Password=sa;";
        }


    }
}