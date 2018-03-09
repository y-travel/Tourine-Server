CREATE TABLE [dbo].[TourService]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Service_Id] DEFAULT (newid()),
[ServiceType] [tinyint] NULL,
[Price] [bigint] NULL,
[Status] [tinyint] NULL,
[TourDetailId] [uniqueidentifier] NOT NULL
)
GO
ALTER TABLE [dbo].[TourService] ADD CONSTRAINT [PK_Service_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[TourService] ADD CONSTRAINT [FK_Service_TourDetail] FOREIGN KEY ([TourDetailId]) REFERENCES [dbo].[TourDetail] ([Id])
GO
