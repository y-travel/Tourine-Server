CREATE TABLE [dbo].[Person]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Person_Id] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL,
[Family] [nvarchar] (50) NOT NULL,
[MobileNumber] [varchar] (11) NULL,
[NationalCode] [varchar] (10) NULL,
[BirthDate] [date] NULL,
[PassportExpireDate] [date] NULL,
[PassportNo] [varchar] (50) NULL,
[Gender] [bit] NOT NULL,
[Type] [tinyint] NOT NULL,
[SocialNumber] [varchar] (15) NULL,
[ChatId] [bigint] NULL,
[IsUnder5] [bit] NULL,
[IsInfant] [bit] NULL,
[VisaExpireDate] [date] NULL,
[EnglishName] [varchar] (50) NULL,
[EnglishFamily] [varchar] (50) NULL
)
GO
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [PK_Person_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
