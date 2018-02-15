CREATE TABLE [dbo].[Agency]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Agency_Id] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL,
[PhoneNumber] [varchar] (11) NULL
)
GO
ALTER TABLE [dbo].[Agency] ADD CONSTRAINT [PK_Agency_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
