CREATE TABLE [dbo].[Passenger]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Passenger_Id] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL,
[Family] [nvarchar] (50) NOT NULL,
[MobileNumber] [varchar] (11) NULL,
[NationalCode] [varchar] (10) NOT NULL,
[BirthDate] [date] NOT NULL,
[PassportExpireDate] [date] NULL,
[PassportNo] [varchar] (50) NULL,
[Gender] [bit] NOT NULL,
[Type] [tinyint] NOT NULL,
[SocialNumber] [varchar] (15) NULL,
[ChatId] [varchar] (50) NULL
)
GO
ALTER TABLE [dbo].[Passenger] ADD CONSTRAINT [PK_Passenger_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
