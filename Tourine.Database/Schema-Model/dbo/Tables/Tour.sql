CREATE TABLE [dbo].[Tour]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Tour_ID] DEFAULT (newid()),
[Capacity] [int] NULL,
[BasePrice] [bigint] NULL,
[ParentId] [uniqueidentifier] NULL,
[Code] [nvarchar] (50) NULL,
[Status] [tinyint] NOT NULL,
[TourDetailId] [uniqueidentifier] NULL,
[AgencyId] [uniqueidentifier] NULL,
[InfantPrice] [bigint] NULL,
[CreationDate] [datetime] NULL
)
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [PK_Tour_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_Agency_Id] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_Tour_Id] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Tour] ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_TourDetail_Id] FOREIGN KEY ([TourDetailId]) REFERENCES [dbo].[TourDetail] ([Id]) ON DELETE CASCADE
GO
