using NUnit.Framework;
using tSQLt.Helpers;

namespace tSQLt
{
	[TestFixture]
	public class UnitTests_AgeCalc
	{
		[TestFixtureSetUp]
		public void SetupTestFixture()
		{			
			TestBase.SetupTestFixture();
		}
        
		[SetUp]
		public void SetupTest()
		{
			TestBase.SetupTest();
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenAtBirth_ShouldReturn_0()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenAtBirth_ShouldReturn_0]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenBornOnLeapYearDayAfterFirstBirthday_ShouldReturn_1()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenBornOnLeapYearDayAfterFirstBirthday_ShouldReturn_1]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenBornOnLeapYearDayBeforeFirstBirthday_ShouldReturn_0()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenBornOnLeapYearDayBeforeFirstBirthday_ShouldReturn_0]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenBornOnLeapYearOnFirstBirthday_ShouldReturn_1()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenBornOnLeapYearOnFirstBirthday_ShouldReturn_1]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenCurrentDateIsNull_ShouldReturn_0()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenCurrentDateIsNull_ShouldReturn_0]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenDayAfterFirstBirthday_ShouldReturn_1()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenDayAfterFirstBirthday_ShouldReturn_1]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenDayBeforeFirstBirthday_ShouldReturn_0()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenDayBeforeFirstBirthday_ShouldReturn_0]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenDOBIsNull_ShouldReturn_0()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenDOBIsNull_ShouldReturn_0]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenHourBeforeDobOnBirthday_ShouldReturn_1()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenHourBeforeDobOnBirthday_ShouldReturn_1]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenNotYetBorn_ShouldReturn_0()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenNotYetBorn_ShouldReturn_0]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenOnFirstBirthday_ShouldReturn_1()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenOnFirstBirthday_ShouldReturn_1]");
		}

		
		[Test]
		public void test_CalculateCurrentAge_GivenOnSecondBirthday_ShouldReturn_2()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenOnSecondBirthday_ShouldReturn_2]");
		}

		
		[Test]
		public void test_LegalDrivingAgeReport_GivenCurrentDate_ShouldLegalDrivers()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_LegalDrivingAgeReport_GivenCurrentDate_ShouldLegalDrivers]");
		}

		
		[Test]
		public void test_LegalDrivingAgeReport_ShouldExist()
		{
			TestBase.RunTest("[UnitTests_AgeCalc].[test_LegalDrivingAgeReport_ShouldExist]");
		}

	}
}