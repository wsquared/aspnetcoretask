/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
/****** Script for SelectTopNRows command from SSMS  ******/

INSERT INTO [Task].[dbo].[Task]
(TaskId, Title, Details, DueDate, CompletedDate)
VALUES
(NEWID(), 'Burn the midnight oil', 'Slogging away', CAST('2016-03-02' as datetime), CAST('2016-03-02' AS datetimeoffset))

INSERT INTO [Task].[dbo].[Task]
(TaskId, Title, Details, DueDate, CompletedDate)
VALUES
(NEWID(), 'Finish backend task', 'Aspnet core level up rocks', CAST('2016-03-07' as datetime), null)

INSERT INTO [Task].[dbo].[Task]
(TaskId, Title, Details, DueDate, CompletedDate)
VALUES
(NEWID(), 'Finish Angular 2 implementation', 'Angular 2 is the way of the future', CAST('2016-03-07' as datetime), null)