
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
			WHERE ROUTINE_SCHEMA='dbo' 
			AND ROUTINE_NAME='CalculateCurrentAge')
BEGIN
	DROP FUNCTION [dbo].[CalculateCurrentAge]
END
GO

CREATE FUNCTION [dbo].[CalculateCurrentAge](@DOB date, @CurrentDate date)
RETURNS INT
AS
BEGIN
DECLARE @Age int = 0
DECLARE @NextBirthday datetime
	
	IF @DOB IS NULL OR @CurrentDate IS NULL RETURN @Age;
	IF @CurrentDate < @DOB RETURN @Age;
		
	SET @Age = DATEDIFF(year, @DOB, @CurrentDate)
	SET @NextBirthday = DATEADD(year, @Age, @DOB)
	IF  @NextBirthday > @CurrentDate 
		SET @Age = @Age - 1;
	
	RETURN @Age;

END

GO

--EXEC tSQLt.Run 'UnitTests_AgeCalc'
--GO