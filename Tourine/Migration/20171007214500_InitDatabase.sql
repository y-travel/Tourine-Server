USE [Tourine]
GO
/****** Object:  Table [dbo].[Agency]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agency](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](50) NULL,
	[PhoneNumber] [varchar](11) NULL,
 CONSTRAINT [PK_Agency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AgencyCustomer]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgencyCustomer](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[AgencyId] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AgencyCustomer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Block]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Block](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Code] [nvarchar](50) NULL,
	[TourID] [uniqueidentifier] NULL,
	[Price] [int] NULL,
	[Capacity] [int] NULL,
	[Parent] [uniqueidentifier] NULL,
	[CustomerId] [uniqueidentifier] NULL,
	[SubmitDate] [datetime] NULL,
 CONSTRAINT [PK_Coppon_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Currency]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Factor] [nvarchar](50) NULL,
 CONSTRAINT [PK_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Family] [nvarchar](50) NULL,
	[MobileNumber] [varchar](15) NULL,
	[Phone] [varchar](15) NULL,
 CONSTRAINT [PK_Reagent_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Destination]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Destination](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Destination_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Passenger]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Passenger](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Family] [nvarchar](50) NULL,
	[MobileNumber] [varchar](11) NULL,
	[NationalCode] [varchar](10) NULL,
	[BirthDate] [date] NOT NULL,
	[PassportExpireDate] [date] NOT NULL,
	[AgencyId] [nchar](10) NULL,
	[PassportNo] [varchar](50) NULL,
 CONSTRAINT [PK_Customer_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PassengerList]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PassengerList](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[BlockId] [uniqueidentifier] NULL,
	[PassengerId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PassengerList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Place]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Place](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Place_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PriceDetail]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceDetail](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[value] [money] NULL,
	[CurrencyID] [int] NULL,
	[TourId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PriceDetail_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tour]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tour](
	[ID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Code] [nvarchar](50) NULL,
	[DestinationID] [uniqueidentifier] NULL,
	[Capacity]  AS ([AdultCount]+[InfantCount]),
	[Status] [tinyint] NULL,
	[Duration] [int] NULL,
	[StartDate] [datetime] NULL,
	[PlaceID] [uniqueidentifier] NULL,
	[isFlight] [bit] NULL,
	[AdultCount] [int] NULL,
	[AdultMinPrice] [int] NULL,
	[BusPrice] [int] NULL,
	[RoomPrice] [int] NULL,
	[FoodPrice] [int] NULL,
	[InfantPrice] [int] NULL,
	[InfantCount] [int] NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_Tour_ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 1/13/2018 8:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[CustomerId] [uniqueidentifier] NULL,
	[Role] [tinyint] NULL,
 CONSTRAINT [PK_User_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Agency] ADD  CONSTRAINT [DF_Agency_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[AgencyCustomer] ADD  CONSTRAINT [DF_AgencyCustomer_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Block] ADD  CONSTRAINT [DF_Block_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Customer] ADD  CONSTRAINT [DF_Customer_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Passenger] ADD  CONSTRAINT [DF_Passenger_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[PassengerList] ADD  CONSTRAINT [DF_PassengerList_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Place] ADD  CONSTRAINT [DF_Place_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[PriceDetail] ADD  CONSTRAINT [DF_PriceDetail_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[Tour] ADD  CONSTRAINT [DF_Tour_ID]  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[AgencyCustomer]  WITH CHECK ADD  CONSTRAINT [FK_AgencyCustomer_Agency] FOREIGN KEY([AgencyId])
REFERENCES [dbo].[Agency] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AgencyCustomer] CHECK CONSTRAINT [FK_AgencyCustomer_Agency]
GO
ALTER TABLE [dbo].[AgencyCustomer]  WITH CHECK ADD  CONSTRAINT [FK_AgencyCustomer_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AgencyCustomer] CHECK CONSTRAINT [FK_AgencyCustomer_Customer]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Block_ID] FOREIGN KEY([Parent])
REFERENCES [dbo].[Block] ([ID])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Block_ID]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Customer]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_Tour_ID] FOREIGN KEY([TourID])
REFERENCES [dbo].[Tour] ([ID])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_Tour_ID]
GO
ALTER TABLE [dbo].[PassengerList]  WITH CHECK ADD  CONSTRAINT [FK_PassengerList_Block] FOREIGN KEY([BlockId])
REFERENCES [dbo].[Block] ([ID])
GO
ALTER TABLE [dbo].[PassengerList] CHECK CONSTRAINT [FK_PassengerList_Block]
GO
ALTER TABLE [dbo].[PassengerList]  WITH CHECK ADD  CONSTRAINT [FK_PassengerList_Passenger] FOREIGN KEY([PassengerId])
REFERENCES [dbo].[Passenger] ([Id])
GO
ALTER TABLE [dbo].[PassengerList] CHECK CONSTRAINT [FK_PassengerList_Passenger]
GO
ALTER TABLE [dbo].[PriceDetail]  WITH CHECK ADD  CONSTRAINT [FK_PriceDetail_Currency_ID] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currency] ([ID])
GO
ALTER TABLE [dbo].[PriceDetail] CHECK CONSTRAINT [FK_PriceDetail_Currency_ID]
GO
ALTER TABLE [dbo].[PriceDetail]  WITH CHECK ADD  CONSTRAINT [FK_PriceDetail_Tour] FOREIGN KEY([TourId])
REFERENCES [dbo].[Tour] ([ID])
GO
ALTER TABLE [dbo].[PriceDetail] CHECK CONSTRAINT [FK_PriceDetail_Tour]
GO
ALTER TABLE [dbo].[Tour]  WITH CHECK ADD  CONSTRAINT [FK_Tour_Destination_ID] FOREIGN KEY([DestinationID])
REFERENCES [dbo].[Destination] ([ID])
GO
ALTER TABLE [dbo].[Tour] CHECK CONSTRAINT [FK_Tour_Destination_ID]
GO
ALTER TABLE [dbo].[Tour]  WITH CHECK ADD  CONSTRAINT [FK_Tour_Place_Id] FOREIGN KEY([PlaceID])
REFERENCES [dbo].[Place] ([Id])
GO
ALTER TABLE [dbo].[Tour] CHECK CONSTRAINT [FK_Tour_Place_Id]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Customer]
GO
