-- <Migration ID="a72cb533-a4f2-4bda-8dcb-c86614badad0" />
GO

PRINT N'Altering [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD
[BuyerIsPassenger] [bit] NULL
GO
