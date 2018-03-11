-- <Migration ID="1276012e-cf37-4b19-adfe-6a08628e7043" />
GO

PRINT N'Altering [dbo].[TourDetail]'
GO
ALTER TABLE [dbo].[TourDetail] DROP
COLUMN [CreationDate]
GO
PRINT N'Altering [dbo].[Tour]'
GO
ALTER TABLE [dbo].[Tour] ADD
[CreationDate] [datetime] NULL
GO
