USE AgeCalculator_Test
GO

EXEC tSQLt.NewTestClass 'UnitTests_AgeCalc'
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[setup]
AS
BEGIN	
  	IF OBJECT_ID(N'tempdb..#Expected') IS NOT NULL DROP TABLE [#Expected];       
	IF OBJECT_ID(N'tempdb..#Actual') IS NOT NULL DROP TABLE [#Actual];  	                    
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenAtBirth_ShouldReturn_0]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04'
DECLARE @CurrentDate datetime = '2013-05-04'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 0, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenNotYetBorn_ShouldReturn_0]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04'
DECLARE @CurrentDate datetime = '2013-05-03'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 0, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenCurrentDateIsNull_ShouldReturn_0]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = GETDATE()
DECLARE @CurrentDate datetime = NULL
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 0, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenDOBIsNull_ShouldReturn_0]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = NULL
DECLARE @CurrentDate datetime = GETDATE()
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 0, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenOnFirstBirthday_ShouldReturn_1]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04'
DECLARE @CurrentDate datetime = '2014-05-04'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 1, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenDayBeforeFirstBirthday_ShouldReturn_0]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04'
DECLARE @CurrentDate datetime = '2014-05-03'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 0, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenHourBeforeDobOnBirthday_ShouldReturn_1]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04 11:30'
DECLARE @CurrentDate datetime = '2014-05-04 10:30'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 1, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenDayAfterFirstBirthday_ShouldReturn_1]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04'
DECLARE @CurrentDate datetime = '2014-05-05'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 1, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenOnSecondBirthday_ShouldReturn_2]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04'
DECLARE @CurrentDate datetime = '2015-05-04'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 2, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenBornOnLeapYearOnFirstBirthday_ShouldReturn_1]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2012-02-29'
DECLARE @CurrentDate datetime = '2013-02-28'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 1, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenBornOnLeapYearDayBeforeFirstBirthday_ShouldReturn_0]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2012-02-29'
DECLARE @CurrentDate datetime = '2013-02-27'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 0, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_CalculateCurrentAge_GivenBornOnLeapYearDayAfterFirstBirthday_ShouldReturn_1]
AS
BEGIN
	-- Arrange
DECLARE @DOB datetime = '2013-05-04'
DECLARE @CurrentDate datetime = '2013-05-04'
DECLARE @result int

	-- Act
	SELECT @result = dbo.CalculateCurrentAge(@DOB, @CurrentDate)
	
	-- Assert
	EXEC tSQLt.AssertEquals 0, @result
END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_LegalDrivingAgeReport_ShouldExist]
AS
BEGIN

	EXEC tSQLt.AssertObjectExists 'dbo.LegalDrivingAgeReport'

END
GO

CREATE PROCEDURE [UnitTests_AgeCalc].[test_LegalDrivingAgeReport_GivenCurrentDate_ShouldLegalDrivers]
AS
BEGIN

DECLARE @currentDate datetime = '2008-05-05'
	
	IF OBJECT_ID(N'tempdb..#Results') IS NOT NULL DROP TABLE [#Results];
	IF OBJECT_ID(N'tempdb..#Actual') IS NOT NULL DROP TABLE [#Actual];	
	
	EXEC tSQLt.FakeTable 'Person'

	-- older than 18
	INSERT Person (FirstName, Surname, IDNumber, DateOfBirth) VALUES ('John', 'Smith', '12345', '1985-03-06')
	-- 18
	INSERT Person (FirstName, Surname, IDNumber, DateOfBirth) VALUES ('Carrol', 'Williams', '2121', '1990-05-05')
	-- Not yet 18
	INSERT Person (FirstName, Surname, IDNumber, DateOfBirth) VALUES ('Bruce', 'Jones', '8888', '1990-05-06')

	SELECT FullName, IDNumber
	INTO #Expected
	FROM dbo.LegalDrivingAgeReport(@currentDate)
	WHERE 1=0
	
	INSERT #Expected (FullName, IDNumber) VALUES ('Smith, John', '12345')
	INSERT #Expected (FullName, IDNumber) VALUES ('Williams, Carrol', '2121')
	
	-- Act
	SELECT FullName, IDNumber
	INTO #Actual
	FROM dbo.LegalDrivingAgeReport(@currentDate)

	-- Assert
	EXEC tSQLt.AssertEqualsTable '#Expected', '#Actual'
	
END
GO