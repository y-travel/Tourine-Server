CREATE TABLE [dbo].[TourOption]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Option_Id] DEFAULT (newid()),
[OptionType] [tinyint] NULL,
[Price] [bigint] NULL,
[OptionStatus] [tinyint] NULL,
[TourId] [uniqueidentifier] NOT NULL
)
GO
ALTER TABLE [dbo].[TourOption] ADD CONSTRAINT [PK_Option_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[TourOption] ADD CONSTRAINT [FK_Option_Tour] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
