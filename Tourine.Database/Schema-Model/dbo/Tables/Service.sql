CREATE TABLE [dbo].[Service]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Service__Id__4D94879B] DEFAULT (newid()),
[PassengerId] [uniqueidentifier] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[Type] [tinyint] NOT NULL,
[Status] [tinyint] NOT NULL
)
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [PK_Service_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [FK_Service_Passenger_Id] FOREIGN KEY ([PassengerId]) REFERENCES [dbo].[Passenger] ([Id])
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [FK_Service_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
