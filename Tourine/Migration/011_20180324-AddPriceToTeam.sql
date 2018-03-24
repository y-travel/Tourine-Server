-- <Migration ID="f25f80f1-869d-4f65-a165-08b3dcabec85" />
GO

PRINT N'Altering [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD
[InfantPrice] [bigint] NULL,
[BasePrice] [bigint] NULL,
[TotalPrice] [bigint] NULL
GO
PRINT N'Altering [dbo].[PassengerList]'
GO
EXEC sp_rename N'[dbo].[PassengerList].[VisaDelivered]', N'HaveVisa', N'COLUMN'
GO
