using Microsoft.EntityFrameworkCore.Migrations;

namespace Projects.Data.Migrations
{
    public partial class AddsInitializeDatabaseProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE OR ALTER PROCEDURE initialize_database
AS
BEGIN
-- Cleare the tables
DELETE FROM TimeLogs
DBCC CHECKIDENT (TimeLogs, RESEED, 0)


DELETE FROM Users
DBCC CHECKIDENT (Users, RESEED, 0)


DELETE FROM Projects
DBCC CHECKIDENT (Projects, RESEED, 0)

-- Generate Users
DECLARE @firstName VARCHAR(50)
DECLARE @lastName VARCHAR(50)
DECLARE @domain VARCHAR(100)

DECLARE @counter INT = 1
DECLARE @max INT = 100

WHILE @counter <= 100
BEGIN

	SET @firstName = (SELECT TOP 1 FirstName 
		FROM (SELECT 'John' AS FirstName 
		UNION SELECT 'Gringo' AS FirstName 
		UNION SELECT 'Mark' AS FirstName
		UNION SELECT 'Lisa' AS FirstName
		UNION SELECT 'Maria' AS FirstName
		UNION SELECT 'Sonya' AS FirstName
		UNION SELECT 'Philip' AS FirstName
		UNION SELECT 'Jose' AS FirstName
		UNION SELECT 'Lorenzo' AS FirstName
		UNION SELECT 'George' AS FirstName
		UNION SELECT 'Justin' AS FirstName) AS First_Names ORDER BY NEWID())

	SET @lastName = (SELECT TOP 1 LastName 
		FROM (SELECT 'Johnson' AS LastName 
		UNION SELECT 'Lamas' AS LastName 
		UNION SELECT 'Jackson' AS LastName
		UNION SELECT 'Brown' AS LastName
		UNION SELECT 'Mason' AS LastName
		UNION SELECT 'Rodriguez' AS LastName
		UNION SELECT 'Roberts' AS LastName
		UNION SELECT 'Thomas' AS LastName
		UNION SELECT 'Rose' AS LastName
		UNION SELECT 'McDonalds' AS LastName) AS Last_Names ORDER BY NEWID())

	SET @domain = (SELECT TOP 1 [Domains] 
		FROM (SELECT 'hotmail.com' AS [Domains] 
		UNION SELECT 'gmail.com' AS [Domains]
		UNION SELECT 'live.com' AS [Domains]) AS [Domains] ORDER BY NEWID())


	INSERT INTO Users (FirstName, LastName, Email) VALUES
	(@firstName, @lastName, @firstName + '.' + @lastName + '@' + @domain)

	SET @counter = @counter + 1
END


-- Insert Projects
INSERT INTO Projects (ProjectId, [Name]) VALUES
(NEWID(), 'My own'),
(NEWID(), 'Free Time'),
(NEWID(), 'Work')


-- Generate TimeLogs

DECLARE @userId INT
DECLARE @getUserId CURSOR
SET @getUserId = CURSOR FOR
SELECT Id
FROM Users

OPEN @getUserId
FETCH NEXT
FROM @getUserId INTO @userId

WHILE @@FETCH_STATUS = 0
BEGIN

	DECLARE @maxLogs INT = CAST(ROUND(RAND() * (20 - 1) + 1, 0) AS INT)
	DECLARE @maxSum FLOAT = 8.00

	DECLARE @FromDate DATE = '2022-01-01'
	DECLARE @ToDate DATE =  GETDATE()

	DECLARE @currLog INT = 1


	DECLARE @currDate DATE = DATEADD(DAY, 
				   RAND(CHECKSUM(NEWID()))*(1+DATEDIFF(DAY, @FromDate, @ToDate)), 
				   @FromDate)

	WHILE @currLog <= @maxLogs
	BEGIN

		SET @currDate = DATEADD(DAY, 
				   RAND(CHECKSUM(NEWID()))*(1+DATEDIFF(DAY, @FromDate, @ToDate)), 
				   @FromDate)

		DECLARE @sum FLOAT = ROUND(RAND() * (8 - 0.25) + 0.25, 2)

		IF (SELECT SUM(HoursWorked) + @sum FROM TimeLogs WHERE UserId = @userId AND [Date] = @currDate) > @maxSum CONTINUE

		DECLARE @projectId INT = (SELECT TOP 1 Id FROM (SELECT Id FROM Projects) AS Current_Projects ORDER BY NEWID())

		INSERT INTO TimeLogs ([Date], HoursWorked, ProjectId, UserId) VALUES
		(@currDate, @sum, @projectId, @userId);

		SET @currLog = @currLog + 1

	END

	SET @currLog = 1

	FETCH NEXT
	FROM @getUserId INTO @userId

END

CLOSE @getUserId
DEALLOCATE @getUserId

END";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = @"DROP PROC IF EXISTS initialize_database";

            migrationBuilder.Sql(sql);
        }
    }
}
