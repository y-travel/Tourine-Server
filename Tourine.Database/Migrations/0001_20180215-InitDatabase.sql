-- <Migration ID="34b207a3-c620-4983-9cbb-60322e879be4" />
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
PRINT N'Creating [dbo].[Passenger]'
GO
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
PRINT N'Creating primary key [PK_Passenger_Id] on [dbo].[Passenger]'
GO
ALTER TABLE [dbo].[Passenger] ADD CONSTRAINT [PK_Passenger_Id] PRIMARY KEY CLUSTERED  ([Id])
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
[InfantPrice] [bigint] NULL,
[BusPrice] [bigint] NULL,
[RoomPrice] [bigint] NULL,
[FoodPrice] [bigint] NULL,
[CreationDate] [datetime] NOT NULL,
[LeaderId] [uniqueidentifier] NULL
)
GO
PRINT N'Creating primary key [PK_TourDetail_Id] on [dbo].[TourDetail]'
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [PK_TourDetail_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Customer]'
GO
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
PRINT N'Creating primary key [PK_Customer_Id] on [dbo].[Customer]'
GO
ALTER TABLE [dbo].[Customer] ADD CONSTRAINT [PK_Customer_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[User]'
GO
CREATE TABLE [dbo].[User]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_User_Id] DEFAULT (newid()),
[Username] [nvarchar] (50) NOT NULL,
[Password] [nvarchar] (50) NOT NULL,
[CustomerId] [uniqueidentifier] NOT NULL,
[Role] [tinyint] NOT NULL
)
GO
PRINT N'Creating primary key [PK_User_Id] on [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [PK_User_Id] PRIMARY KEY CLUSTERED  ([Id])
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
[AgencyId] [uniqueidentifier] NULL
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
[Price] [bigint] NULL,
[Buyer] [uniqueidentifier] NOT NULL,
[Count] [int] NULL,
[SubmitDate] [datetime] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Team_Id] on [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [PK_Team_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[TeamPassenger]'
GO
CREATE TABLE [dbo].[TeamPassenger]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__TeamPassenge__Id__48CFD27E] DEFAULT (newid()),
[TeamId] [uniqueidentifier] NOT NULL,
[PassengerId] [uniqueidentifier] NOT NULL
)
GO
PRINT N'Creating primary key [PK_TeamPassenger_Id] on [dbo].[TeamPassenger]'
GO
ALTER TABLE [dbo].[TeamPassenger] ADD CONSTRAINT [PK_TeamPassenger_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
PRINT N'Creating [dbo].[Service]'
GO
CREATE TABLE [dbo].[Service]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Service__Id__4D94879B] DEFAULT (newid()),
[PassengerId] [uniqueidentifier] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[Type] [tinyint] NOT NULL,
[Status] [tinyint] NOT NULL
)
GO
PRINT N'Creating primary key [PK_Service_Id] on [dbo].[Service]'
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [PK_Service_Id] PRIMARY KEY CLUSTERED  ([Id])
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
PRINT N'Creating [dbo].[AgencyCustomer]'
GO
CREATE TABLE [dbo].[AgencyCustomer]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_AgencyCustomer_Id] DEFAULT (newid()),
[AgencyId] [uniqueidentifier] NOT NULL,
[CustomerId] [uniqueidentifier] NOT NULL
)
GO
PRINT N'Creating primary key [PK_AgencyCustomer] on [dbo].[AgencyCustomer]'
GO
ALTER TABLE [dbo].[AgencyCustomer] ADD CONSTRAINT [PK_AgencyCustomer] PRIMARY KEY CLUSTERED  ([Id])
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
PRINT N'Adding foreign keys to [dbo].[AgencyCustomer]'
GO
ALTER TABLE [dbo].[AgencyCustomer] ADD CONSTRAINT [FK_AgencyCustomer_Agency_Id] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[AgencyCustomer] ADD CONSTRAINT [FK_AgencyCustomer_Customer_Id] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
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
PRINT N'Adding foreign keys to [dbo].[User]'
GO
ALTER TABLE [dbo].[User] ADD CONSTRAINT [FK_User_Customer_Id] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[TourDetail]'
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Destination_Id] FOREIGN KEY ([DestinationId]) REFERENCES [dbo].[Destination] ([Id])
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Passenger_Id] FOREIGN KEY ([LeaderId]) REFERENCES [dbo].[Passenger] ([Id])
GO
ALTER TABLE [dbo].[TourDetail] ADD CONSTRAINT [FK_TourDetail_Place_Id] FOREIGN KEY ([PlaceId]) REFERENCES [dbo].[Place] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[Service]'
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [FK_Service_Passenger_Id] FOREIGN KEY ([PassengerId]) REFERENCES [dbo].[Passenger] ([Id])
GO
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [FK_Service_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Passenger_Id] FOREIGN KEY ([Buyer]) REFERENCES [dbo].[Passenger] ([Id])
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
PRINT N'Adding foreign keys to [dbo].[TeamPassenger]'
GO
ALTER TABLE [dbo].[TeamPassenger] ADD CONSTRAINT [FK_TeamPassenger_Passenger_Id] FOREIGN KEY ([PassengerId]) REFERENCES [dbo].[Passenger] ([Id])
GO
ALTER TABLE [dbo].[TeamPassenger] ADD CONSTRAINT [FK_TeamPassenger_Team_Id] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
GO
