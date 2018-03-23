-- <Migration ID="f5a0676b-1630-4a61-83d7-d30b2ebd7e27" />
GO

PRINT N'Altering [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] ADD
[EnglishName] [varchar] (50) NULL,
[EnglishFamily] [varchar] (50) NULL
GO
