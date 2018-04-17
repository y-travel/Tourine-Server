-- <Migration ID="e7d4657a-57a3-45f9-aa71-46ef75851f09" />
GO

PRINT N'Altering [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD
[IsPending] [bit] NULL
GO
