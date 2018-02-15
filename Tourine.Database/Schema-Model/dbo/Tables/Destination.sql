CREATE TABLE [dbo].[Destination]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Destination__Id__2A4B4B5E] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL
)
GO
ALTER TABLE [dbo].[Destination] ADD CONSTRAINT [PK_Destination_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
