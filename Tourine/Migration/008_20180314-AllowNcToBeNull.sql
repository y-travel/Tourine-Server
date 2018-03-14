-- <Migration ID="badc6652-1971-406f-b678-c68d51114fdd" />
GO

PRINT N'Altering [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] ALTER COLUMN [NationalCode] [varchar] (10) NULL
GO
ALTER TABLE [dbo].[Person] ALTER COLUMN [BirthDate] [date] NULL
GO
