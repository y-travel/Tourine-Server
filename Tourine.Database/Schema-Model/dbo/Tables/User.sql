CREATE TABLE [dbo].[User]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_User_Id] DEFAULT (newid()),
[Username] [nvarchar] (50) NOT NULL,
[Password] [nvarchar] (50) NOT NULL,
[PersonId] [uniqueidentifier] NOT NULL,
[Role] [tinyint] NOT NULL
)
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
