
-- <Migration ID="669336b9-b606-4c2c-b30a-84ec5cf80a7b" />
GO

CREATE TABLE [dbo].[PassengerList]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PassengerList__Id__4D94879B] DEFAULT (newid()),
[PersonId] [uniqueidentifier] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[Type] [bigint] NOT NULL,
[Status] [tinyint] NOT NULL,
[Price] [bigint] NOT NULL,
[CurrencyFactor] [float] NOT NULL
)
GO
PRINT N'Creating primary key [PK_PassengerList_Id] on [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [PK_PassengerList_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Copying data from [dbo].[Service] to [dbo].[PassengerList]'
GO
INSERT INTO PassengerList 
  SELECT * FROM [Service]  sv
GO
PRINT N'Creating [dbo].[TourOption]'
GO
CREATE TABLE [dbo].[TourOption]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Option_Id] DEFAULT (newid()),
[OptionType] [tinyint] NULL,
[Price] [bigint] NULL,
[Status] [tinyint] NULL,
[TourId] [uniqueidentifier] NOT NULL
)
GO

insert INTO TourOption ( OptionType, Price,Status, TourId)
  SELECT 1,BusPrice,1,Tour.Id FROM TourDetail INNER JOIN tour ON TourDetail.Id=tour.TourDetailId AND ParentId IS not null
GO
 insert INTO TourOption ( OptionType, Price,Status, TourId)
    SELECT 2,FoodPrice,2,Tour.Id FROM TourDetail INNER JOIN tour ON TourDetail.Id=tour.TourDetailId AND ParentId IS not null
GO
 insert INTO TourOption ( OptionType, Price,Status, TourId)
    SELECT 4,RoomPrice,1,Tour.Id FROM TourDetail INNER JOIN tour ON TourDetail.Id=tour.TourDetailId AND ParentId IS not null
Go
PRINT N'Copying prices from [dbo].[Service] to [TourOption]'
GO
PRINT N'Dropping foreign keys from [dbo].[Service]'
GO
ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_Person_Id]
GO
ALTER TABLE [dbo].[Service] DROP CONSTRAINT [FK_Service_Tour_Id]
GO
PRINT N'Dropping constraints from [dbo].[Service]'
GO
ALTER TABLE [dbo].[Service] DROP CONSTRAINT [PK_Service_Id]
GO
PRINT N'Dropping constraints from [dbo].[Service]'
GO
ALTER TABLE [dbo].[Service] DROP CONSTRAINT [DF__Service__Id__4D94879B]
GO
PRINT N'Dropping [dbo].[Service]'
GO
DROP TABLE [dbo].[Service]
GO
PRINT N'Altering [dbo].[TourDetail]'
GO
ALTER TABLE [dbo].[TourDetail] DROP
COLUMN [InfantPrice],
COLUMN [BusPrice],
COLUMN [RoomPrice],
COLUMN [FoodPrice]
GO
PRINT N'Altering [dbo].[Tour]'
GO
ALTER TABLE [dbo].[Tour] ADD
[InfantPrice] [bigint] NULL
GO
PRINT N'Creating [dbo].[PassengerList]'
GO
PRINT N'Adding foreign keys to [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[TourOption]'
GO
ALTER TABLE [dbo].[TourOption] ADD CONSTRAINT [FK_Option_Tour] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
PRINT N'Creating primary key [PK_Option_Id] on [dbo].[TourOption]'
GO
ALTER TABLE [dbo].[TourOption] ADD CONSTRAINT [PK_Option_Id] PRIMARY KEY CLUSTERED  ([Id])
GO