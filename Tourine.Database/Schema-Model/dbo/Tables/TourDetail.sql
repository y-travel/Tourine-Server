CREATE TABLE [dbo].[TourDetail]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_TourDetail_Id] DEFAULT (newid()),
[DestinationId] [uniqueidentifier] NOT NULL,
[Duration] [int] NULL,
[StartDate] [datetime] NULL,
[PlaceId] [uniqueidentifier] NOT NULL,
[IsFlight] [bit] NULL,
[InfantPrice] [bigint] NULL,
[BusPrice] [bigint] NULL,
[RoomPrice] [bigint] NULL,
[FoodPrice] [bigint] NULL,
[CreationDate] [datetime] NOT NULL,
[LeaderId] [uniqueidentifier] NULL
)
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [PK_TourDetail_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Destination_Id] FOREIGN KEY ([DestinationId]) REFERENCES [dbo].[Destination] ([Id])
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Person_Id] FOREIGN KEY ([LeaderId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Place_Id] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Place] ([Id])
GO
