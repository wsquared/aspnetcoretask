CREATE TABLE [dbo].[Task] (
    [Sequence]      INT                IDENTITY (1, 1) NOT NULL,
    [TaskId]        UNIQUEIDENTIFIER   NOT NULL,
    [Title]         VARCHAR (MAX)      NOT NULL,
    [Details]       VARCHAR(MAX)	   NULL,
    [DueDate]       DATE               NULL,
    [CompletedDate] DATETIMEOFFSET (7) NULL,
    PRIMARY KEY CLUSTERED ([TaskId] ASC)
);
