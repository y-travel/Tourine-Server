-- <Migration ID="9512d682-6c8f-4b42-a011-1a5e6dfd5170" />
GO

PRINT N'Dropping foreign keys from [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] DROP CONSTRAINT [FK_PassengerList_Team_Id]
GO
PRINT N'Dropping foreign keys from [dbo].[PriceDetail]'
GO
ALTER TABLE [dbo].[PriceDetail] DROP CONSTRAINT [FK_PriceDetail_Tour_Id]
GO
PRINT N'Dropping foreign keys from [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] DROP CONSTRAINT [FK_Team_Tour_Id]
GO
PRINT N'Dropping foreign keys from [dbo].[TourOption]'
GO
ALTER TABLE [dbo].[TourOption] DROP CONSTRAINT [FK_Option_Tour]
GO
PRINT N'Dropping foreign keys from [dbo].[Tour]'
GO
ALTER TABLE [dbo].[Tour] DROP CONSTRAINT [FK_Tour_TourDetail_Id]
GO
PRINT N'Adding foreign keys to [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Team_Id] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id]) ON DELETE CASCADE
GO
PRINT N'Adding foreign keys to [dbo].[PriceDetail]'
GO
ALTER TABLE [dbo].[PriceDetail] ADD CONSTRAINT [FK_PriceDetail_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id]) ON DELETE CASCADE
GO
PRINT N'Adding foreign keys to [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id]) ON DELETE CASCADE
GO
PRINT N'Adding foreign keys to [dbo].[TourOption]'
GO
ALTER TABLE [dbo].[TourOption] ADD CONSTRAINT [FK_Option_Tour] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id]) ON DELETE CASCADE
GO
PRINT N'Adding foreign keys to [dbo].[Tour]'
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_TourDetail_Id] FOREIGN KEY ([TourDetailId]) REFERENCES [dbo].[TourDetail] ([Id]) ON DELETE CASCADE
GO
