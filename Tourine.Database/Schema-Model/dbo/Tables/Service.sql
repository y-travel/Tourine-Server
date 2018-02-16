CREATE TABLE [dbo].[Service]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Service__Id__4D94879B] DEFAULT (newid()),
[PersonId] [uniqueidentifier] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[Type] [bigint] NOT NULL,
[Status] [tinyint] NOT NULL,
[Price] [bigint] NOT NULL,
[CurrencyFactor] [float] NOT NULL
)
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [PK_Service_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [FK_Service_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [FK_Service_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
