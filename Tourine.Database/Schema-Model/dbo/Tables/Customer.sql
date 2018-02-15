CREATE TABLE [dbo].[Customer]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Customer_Id] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL,
[Family] [nvarchar] (50) NOT NULL,
[MobileNumber] [varchar] (15) NULL,
[Phone] [varchar] (15) NULL,
[SocialNumber] [varchar] (15) NULL,
[ChatId] [varchar] (50) NULL
)
GO
ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [PK_Customer_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
