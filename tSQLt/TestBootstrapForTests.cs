using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace tSQLt
{
    [TestFixture]
    public class TestBootstrapForTests
    {
        [Test]
        [Ignore("Use this test to check that the bootstrapper isn't failing")]
        public void BootstrapForTests_ShouldRun()
        {
            //---------------Set up test pack-------------------
            //---------------Execute Test ----------------------
            BootstrapForTests.BootstrapTestsDatabase();
            //---------------Test Result -----------------------
        }

    }
}