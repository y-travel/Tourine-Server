CREATE TABLE [dbo].[AgencyPerson]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_AgencyPerson_Id] DEFAULT (newid()),
[AgencyId] [uniqueidentifier] NOT NULL,
[PersonId] [uniqueidentifier] NOT NULL
)
GO
ALTER TABLE [dbo].[AgencyPerson] ADD CONSTRAINT [PK_AgencyPerson] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[AgencyPerson] ADD CONSTRAINT [FK_AgencyPerson_Agency_Id] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[AgencyPerson] ADD CONSTRAINT [FK_AgencyPerson_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
