
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES 
			WHERE ROUTINE_SCHEMA='dbo' 
			AND ROUTINE_NAME='LegalDrivingAgeReport')
BEGIN
	DROP FUNCTION [dbo].[LegalDrivingAgeReport]
END
GO

CREATE FUNCTION [dbo].[LegalDrivingAgeReport]
(
	@currentDate datetime
)
RETURNS table
AS 
RETURN
(
SELECT FullName, IDNumber 
FROM 
	(
	SELECT 
		Surname + ', ' + FirstName AS FullName, 
		IDNumber, 
		dbo.CalculateCurrentAge(DateOfBirth, @currentDate) AS Age
	FROM Person
	) rpt
WHERE Age >= 18
)

GO

--EXEC tSQLt.Run 'UnitTests_AgeCalc'
--GO