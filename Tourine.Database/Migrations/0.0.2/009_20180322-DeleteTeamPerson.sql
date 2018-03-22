-- <Migration ID="22877a3e-265e-41f3-801f-9c9b0382facb" />
GO

PRINT N'Dropping foreign keys from [dbo].[TeamPerson]'
GO
ALTER TABLE [dbo].[TeamPerson] DROP CONSTRAINT [FK_TeamPerson_Team_Id]
GO
ALTER TABLE [dbo].[TeamPerson] DROP CONSTRAINT [FK_TeamPerson_Person_Id]
GO
PRINT N'Dropping constraints from [dbo].[TeamPerson]'
GO
ALTER TABLE [dbo].[TeamPerson] DROP CONSTRAINT [PK_TeamPerson_Id]
GO
PRINT N'Dropping constraints from [dbo].[TeamPerson]'
GO
ALTER TABLE [dbo].[TeamPerson] DROP CONSTRAINT [DF__TeamPassenge__Id__48CFD27E]
GO
PRINT N'Dropping [dbo].[TeamPerson]'
GO
DROP TABLE [dbo].[TeamPerson]
GO
PRINT N'Altering [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD
[TeamId] [uniqueidentifier] NULL
GO
PRINT N'Adding foreign keys to [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Team_Id] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
GO
