-- <Migration ID="fce965a7-f052-4bd3-9295-f253cdc27a22" />
GO

PRINT N'Dropping foreign keys from [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] DROP CONSTRAINT [FK_Team_Person_Id]
GO
PRINT N'Altering [dbo].[Team]'
GO
EXEC sp_rename N'[dbo].[Team].[Buyer]', N'BuyerId', N'COLUMN'
GO
PRINT N'Adding foreign keys to [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Person_Id] FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[Person] ([Id])
GO
