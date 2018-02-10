﻿--
-- Script was generated by Devart dbForge Studio for SQL Server, Version 5.3.56.0
-- Product home page: http://www.devart.com/dbforge/sql/studio
-- Script date 11/11/1396 04:00:00 ب.ظ
-- Server version: 13.00.4001
-- Client version: 
--



USE Tourine
GO

IF DB_NAME() <> N'Tourine' SET NOEXEC ON
GO

--
-- Create table "dbo.Place"
--
PRINT (N'Create table "dbo.Place"')
GO
CREATE TABLE dbo.Place (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_Place_Id DEFAULT (newid()) ROWGUIDCOL,
  Name nvarchar(50) NOT NULL,
  CONSTRAINT PK_Place_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Passenger"
--
PRINT (N'Create table "dbo.Passenger"')
GO
CREATE TABLE dbo.Passenger (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_Passenger_Id DEFAULT (newid()) ROWGUIDCOL,
  Name nvarchar(50) NOT NULL,
  Family nvarchar(50) NOT NULL,
  MobileNumber varchar(11) NULL,
  NationalCode varchar(10) NOT NULL,
  BirthDate date NOT NULL,
  PassportExpireDate date NULL,
  PassportNo varchar(50) NULL,
  Gender bit NOT NULL,
  Type tinyint NOT NULL,
  CONSTRAINT PK_Passenger_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Destination"
--
PRINT (N'Create table "dbo.Destination"')
GO
CREATE TABLE dbo.Destination (
  Id uniqueidentifier NOT NULL DEFAULT (newid()),
  Name nvarchar(50) NOT NULL,
  CONSTRAINT PK_Destination_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.TourDetail"
--
PRINT (N'Create table "dbo.TourDetail"')
GO
CREATE TABLE dbo.TourDetail (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_TourDetail_Id DEFAULT (newid()) ROWGUIDCOL,
  DestinationId uniqueidentifier NOT NULL,
  Duration int NULL,
  StartDate datetime NULL,
  PlaceId uniqueidentifier NOT NULL,
  IsFlight bit NULL,
  InfantPrice bigint NULL,
  BusPrice bigint NULL,
  RoomPrice bigint NULL,
  FoodPrice bigint NULL,
  SubmitDate datetime NOT NULL,
  CONSTRAINT PK_TourDetail_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_TourDetail_Destination_Id FOREIGN KEY (DestinationId) REFERENCES dbo.Destination (Id),
  CONSTRAINT FK_TourDetail_Place_Id FOREIGN KEY (PlaceId) REFERENCES dbo.Place (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Customer"
--
PRINT (N'Create table "dbo.Customer"')
GO
CREATE TABLE dbo.Customer (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_Customer_Id DEFAULT (newid()) ROWGUIDCOL,
  Name nvarchar(50) NOT NULL,
  Family nvarchar(50) NOT NULL,
  MobileNumber varchar(15) NULL,
  Phone varchar(15) NULL,
  CONSTRAINT PK_Customer_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.[User]"
--
PRINT (N'Create table "dbo.[User]"')
GO
CREATE TABLE dbo.[User] (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_User_Id DEFAULT (newid()) ROWGUIDCOL,
  Username nvarchar(50) NOT NULL,
  Password nvarchar(50) NOT NULL,
  CustomerId uniqueidentifier NOT NULL,
  Role tinyint NOT NULL,
  CONSTRAINT PK_User_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_User_Customer_Id FOREIGN KEY (CustomerId) REFERENCES dbo.Customer (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Currency"
--
PRINT (N'Create table "dbo.Currency"')
GO
CREATE TABLE dbo.Currency (
  Id int NOT NULL,
  Name nvarchar(50) NOT NULL,
  Factor float NULL,
  CONSTRAINT PK_Currency_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Agency"
--
PRINT (N'Create table "dbo.Agency"')
GO
CREATE TABLE dbo.Agency (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_Agency_Id DEFAULT (newid()) ROWGUIDCOL,
  Name nvarchar(50) NOT NULL,
  PhoneNumber varchar(11) NULL,
  CONSTRAINT PK_Agency_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Tour"
--
PRINT (N'Create table "dbo.Tour"')
GO
CREATE TABLE dbo.Tour (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_Tour_ID DEFAULT (newid()) ROWGUIDCOL,
  Capacity int NULL,
  BasePrice bigint NULL,
  ParentId uniqueidentifier NULL,
  Code nvarchar(50) NULL,
  Status tinyint NOT NULL,
  TourDetailId uniqueidentifier NULL,
  AgencyId uniqueidentifier NULL,
  CreationDate datetime NOT NULL,
  CONSTRAINT PK_Tour_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_Tour_Agency_Id FOREIGN KEY (AgencyId) REFERENCES dbo.Agency (Id),
  CONSTRAINT FK_Tour_TourDetail_Id FOREIGN KEY (TourDetailId) REFERENCES dbo.TourDetail (Id)
)
ON [PRIMARY]
GO

--
-- Create foreign key "FK_Tour_Tour_ID" on table "dbo.Tour"
--
PRINT (N'Create foreign key "FK_Tour_Tour_Id" on table "dbo.Tour"')
GO
ALTER TABLE dbo.Tour WITH CHECK
  ADD CONSTRAINT FK_Tour_Tour_Id FOREIGN KEY (ParentId) REFERENCES dbo.Tour (Id)
GO

--
-- Create table "dbo.Team"
--
PRINT (N'Create table "dbo.Team"')
GO
CREATE TABLE dbo.Team (
  Id uniqueidentifier NOT NULL DEFAULT (newid()),
  TourId uniqueidentifier NOT NULL,
  Price bigint NULL,
  Buyer uniqueidentifier NOT NULL,
  Count int NULL,
  SubmitDate datetime NOT NULL,
  LeaderId uniqueidentifier NULL,
  CONSTRAINT PK_Team_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_Team_Passenger_Id FOREIGN KEY (Buyer) REFERENCES dbo.Passenger (Id),
  CONSTRAINT FK_Team_Passenger_Leader_Id FOREIGN KEY (LeaderId) REFERENCES dbo.Passenger (Id),
  CONSTRAINT FK_Team_Tour_Id FOREIGN KEY (TourId) REFERENCES dbo.Tour (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.TeamPassenger"
--
PRINT (N'Create table "dbo.TeamPassenger"')
GO
CREATE TABLE dbo.TeamPassenger (
  Id uniqueidentifier NOT NULL DEFAULT (newid()),
  TeamId uniqueidentifier NOT NULL,
  PassengerId uniqueidentifier NOT NULL,
  CONSTRAINT PK_TeamPassenger_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_TeamPassenger_Passenger_Id FOREIGN KEY (PassengerId) REFERENCES dbo.Passenger (Id),
  CONSTRAINT FK_TeamPassenger_Team_Id FOREIGN KEY (TeamId) REFERENCES dbo.Team (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Service"
--
PRINT (N'Create table "dbo.Service"')
GO
CREATE TABLE dbo.Service (
  Id uniqueidentifier NOT NULL DEFAULT (newid()),
  PassengerId uniqueidentifier NOT NULL,
  TourId uniqueidentifier NOT NULL,
  Type tinyint NOT NULL,
  Status tinyint NOT NULL,
  CONSTRAINT PK_Service_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_Service_Passenger_Id FOREIGN KEY (PassengerId) REFERENCES dbo.Passenger (Id),
  CONSTRAINT FK_Service_Tour_Id FOREIGN KEY (TourId) REFERENCES dbo.Tour (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.PriceDetail"
--
PRINT (N'Create table "dbo.PriceDetail"')
GO
CREATE TABLE dbo.PriceDetail (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_PriceDetail_ID DEFAULT (newid()) ROWGUIDCOL,
  Value bigint NOT NULL,
  CurrencyId int NOT NULL,
  TourId uniqueidentifier NOT NULL,
  Title nvarchar(100) NULL,
  CONSTRAINT PK_PriceDetail_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_PriceDetail_Currency_Id FOREIGN KEY (CurrencyId) REFERENCES dbo.Currency (Id),
  CONSTRAINT FK_PriceDetail_Tour_Id FOREIGN KEY (TourId) REFERENCES dbo.Tour (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.AgencyCustomer"
--
PRINT (N'Create table "dbo.AgencyCustomer"')
GO
CREATE TABLE dbo.AgencyCustomer (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_AgencyCustomer_Id DEFAULT (newid()) ROWGUIDCOL,
  AgencyId uniqueidentifier NOT NULL,
  CustomerId uniqueidentifier NOT NULL,
  CONSTRAINT PK_AgencyCustomer PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_AgencyCustomer_Agency_Id FOREIGN KEY (AgencyId) REFERENCES dbo.Agency (Id),
  CONSTRAINT FK_AgencyCustomer_Customer_Id FOREIGN KEY (CustomerId) REFERENCES dbo.Customer (Id)
)
ON [PRIMARY]
GO
SET NOEXEC OFF
GO