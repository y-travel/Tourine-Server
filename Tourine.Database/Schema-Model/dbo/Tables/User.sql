CREATE TABLE [dbo].[User]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_User_Id] DEFAULT (newid()),
[Username] [nvarchar] (50) NOT NULL,
[Password] [nvarchar] (50) NOT NULL,
[CustomerId] [uniqueidentifier] NOT NULL,
[Role] [tinyint] NOT NULL
)
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Customer_Id] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
GO
