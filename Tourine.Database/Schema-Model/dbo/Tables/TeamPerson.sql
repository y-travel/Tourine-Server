CREATE TABLE [dbo].[TeamPerson]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__TeamPassenge__Id__48CFD27E] DEFAULT (newid()),
[TeamId] [uniqueidentifier] NOT NULL,
[PersonId] [uniqueidentifier] NOT NULL
)
GO
ALTER TABLE [dbo].[TeamPerson] ADD CONSTRAINT [PK_TeamPerson_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[TeamPerson] ADD CONSTRAINT [FK_TeamPerson_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[TeamPerson] ADD CONSTRAINT [FK_TeamPerson_Team_Id] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
GO
