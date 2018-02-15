CREATE TABLE [dbo].[TeamPassenger]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__TeamPassenge__Id__48CFD27E] DEFAULT (newid()),
[TeamId] [uniqueidentifier] NOT NULL,
[PassengerId] [uniqueidentifier] NOT NULL
)
GO
ALTER TABLE [dbo].[TeamPassenger] ADD CONSTRAINT [PK_TeamPassenger_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[TeamPassenger] ADD CONSTRAINT [FK_TeamPassenger_Passenger_Id] FOREIGN KEY ([PassengerId]) REFERENCES [dbo].[Passenger] ([Id])
GO
ALTER TABLE [dbo].[TeamPassenger] ADD CONSTRAINT [FK_TeamPassenger_Team_Id] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
GO
