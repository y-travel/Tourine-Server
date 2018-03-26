-- <Migration ID="3293fe93-ddfa-420a-9b9d-d9835326415c" />
GO

PRINT N'Creating [dbo].[Place]'
GO
CREATE TABLE [dbo].[Place]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Place_Id] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL
)
GO
PRINT N'Creating primary key [PK_Place_Id] on [dbo].[Place]'
GO
ALTER TABLE [dbo].[Place] ADD CONSTRAINT [PK_Place_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Person]'
GO
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
PRINT N'Creating primary key [PK_Person_Id] on [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] ADD CONSTRAINT [PK_Person_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[User]'
GO
CREATE TABLE [dbo].[User]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_User_Id] DEFAULT (newid()),
[Username] [nvarchar] (50) NOT NULL,
[Password] [nvarchar] (50) NOT NULL,
[PersonId] [uniqueidentifier] NOT NULL,
[Role] [tinyint] NOT NULL
)
GO
PRINT N'Creating primary key [PK_User_Id] on [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Destination]'
GO
CREATE TABLE [dbo].[Destination]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Destination__Id__2A4B4B5E] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL
)
GO
PRINT N'Creating primary key [PK_Destination_Id] on [dbo].[Destination]'
GO
ALTER TABLE [dbo].[Destination] ADD CONSTRAINT [PK_Destination_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[TourDetail]'
GO
CREATE TABLE [dbo].[TourDetail]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_TourDetail_Id] DEFAULT (newid()),
[DestinationId] [uniqueidentifier] NOT NULL,
[Duration] [int] NULL,
[StartDate] [datetime] NULL,
[PlaceId] [uniqueidentifier] NOT NULL,
[IsFlight] [bit] NULL,
[LeaderId] [uniqueidentifier] NULL
)
GO
PRINT N'Creating primary key [PK_TourDetail_Id] on [dbo].[TourDetail]'
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [PK_TourDetail_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Agency]'
GO
CREATE TABLE [dbo].[Agency]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Agency_Id] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL,
[PhoneNumber] [varchar] (11) NULL
)
GO
PRINT N'Creating primary key [PK_Agency_Id] on [dbo].[Agency]'
GO
ALTER TABLE [dbo].[Agency] ADD CONSTRAINT [PK_Agency_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Tour]'
GO
CREATE TABLE [dbo].[Tour]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Tour_ID] DEFAULT (newid()),
[Capacity] [int] NULL,
[BasePrice] [bigint] NULL,
[ParentId] [uniqueidentifier] NULL,
[Code] [nvarchar] (50) NULL,
[Status] [tinyint] NOT NULL,
[TourDetailId] [uniqueidentifier] NULL,
[AgencyId] [uniqueidentifier] NULL,
[InfantPrice] [bigint] NULL,
[CreationDate] [datetime] NULL
)
GO
PRINT N'Creating primary key [PK_Tour_Id] on [dbo].[Tour]'
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [PK_Tour_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Team]'
GO
CREATE TABLE [dbo].[Team]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Team__Id__440B1D61] DEFAULT (newid()),
[TourId] [uniqueidentifier] NOT NULL,
[BuyerId] [uniqueidentifier] NOT NULL,
[Count] [int] NULL,
[SubmitDate] [datetime] NOT NULL,
[InfantPrice] [bigint] NULL,
[BasePrice] [bigint] NULL,
[TotalPrice] [bigint] NULL
)
GO
PRINT N'Creating primary key [PK_Team_Id] on [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [PK_Team_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[PriceDetail]'
GO
CREATE TABLE [dbo].[PriceDetail]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_PriceDetail_ID] DEFAULT (newid()),
[Value] [bigint] NOT NULL,
[CurrencyId] [int] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (100) NULL
)
GO
PRINT N'Creating primary key [PK_PriceDetail_Id] on [dbo].[PriceDetail]'
GO
ALTER TABLE [dbo].[PriceDetail] ADD CONSTRAINT [PK_PriceDetail_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[AgencyPerson]'
GO
CREATE TABLE [dbo].[AgencyPerson]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_AgencyPerson_Id] DEFAULT (newid()),
[AgencyId] [uniqueidentifier] NOT NULL,
[PersonId] [uniqueidentifier] NOT NULL
)
GO
PRINT N'Creating primary key [PK_AgencyPerson] on [dbo].[AgencyPerson]'
GO
ALTER TABLE [dbo].[AgencyPerson] ADD CONSTRAINT [PK_AgencyPerson] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[PassengerList]'
GO
CREATE TABLE [dbo].[PassengerList]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PassengerList__Id__4D94879B] DEFAULT (newid()),
[PersonId] [uniqueidentifier] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[OptionType] [bigint] NOT NULL,
[IncomeStatus] [tinyint] NOT NULL,
[ReceivedMoney] [bigint] NOT NULL,
[CurrencyFactor] [float] NOT NULL,
[PassportDelivered] [bit] NULL,
[TeamId] [uniqueidentifier] NULL,
[HaveVisa] [bit] NULL
)
GO
PRINT N'Creating primary key [PK_PassengerList_Id] on [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [PK_PassengerList_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[TourOption]'
GO
CREATE TABLE [dbo].[TourOption]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Option_Id] DEFAULT (newid()),
[OptionType] [tinyint] NULL,
[Price] [bigint] NULL,
[OptionStatus] [tinyint] NULL,
[TourId] [uniqueidentifier] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Option_Id] on [dbo].[TourOption]'
GO
ALTER TABLE [dbo].[TourOption] ADD CONSTRAINT [PK_Option_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Currency]'
GO
CREATE TABLE [dbo].[Currency]
(
[Id] [int] NOT NULL,
[Name] [nvarchar] (50) NOT NULL,
[Factor] [float] NULL
)
GO
PRINT N'Creating primary key [PK_Currency_Id] on [dbo].[Currency]'
GO
ALTER TABLE [dbo].[Currency] ADD CONSTRAINT [PK_Currency_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[AgencyPerson]'
GO
ALTER TABLE [dbo].[AgencyPerson] ADD CONSTRAINT [FK_AgencyPerson_Agency_Id] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[AgencyPerson] ADD CONSTRAINT [FK_AgencyPerson_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[Tour]'
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_Agency_Id] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_TourDetail_Id] FOREIGN KEY ([TourDetailId]) REFERENCES [dbo].[TourDetail] ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_Tour_Id] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Tour] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[PriceDetail]'
GO
ALTER TABLE [dbo].[PriceDetail] ADD CONSTRAINT [FK_PriceDetail_Currency_Id] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[PriceDetail] ADD CONSTRAINT [FK_PriceDetail_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[TourDetail]'
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Destination_Id] FOREIGN KEY ([DestinationId]) REFERENCES [dbo].[Destination] ([Id])
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Person_Id] FOREIGN KEY ([LeaderId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Place_Id] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Place] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Team_Id] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Person_Id] FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[TourOption]'
GO
ALTER TABLE [dbo].[TourOption] ADD CONSTRAINT [FK_Option_Tour] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
