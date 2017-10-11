

USE Tourine
GO

IF DB_NAME() <> N'Tourine' SET NOEXEC ON
GO

--
-- Create table "dbo.[User]"
--
PRINT (N'Create table "dbo.[User]"')
GO
CREATE TABLE dbo.[User] (
  ID int IDENTITY,
  Name varchar(50) NULL,
  RoleID tinyint NULL,
  CONSTRAINT PK_User_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Unit"
--
PRINT (N'Create table "dbo.Unit"')
GO
CREATE TABLE dbo.Unit (
  ID int IDENTITY,
  Name varchar(50) NULL,
  Factor varchar(50) NULL,
  CONSTRAINT PK_Unit_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Tour"
--
PRINT (N'Create table "dbo.Tour"')
GO
CREATE TABLE dbo.Tour (
  ID bigint IDENTITY,
  Code varchar(50) NULL,
  DestinationID int NULL,
  DatePatternID int NULL,
  Capacity int NULL,
  StatusID int NULL,
  PriceDetailID int NULL,
  CONSTRAINT PK_Tour_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Status"
--
PRINT (N'Create table "dbo.Status"')
GO
CREATE TABLE dbo.Status (
  ID int IDENTITY,
  Name varchar(50) NULL,
  CONSTRAINT PK_Status_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Role"
--
PRINT (N'Create table "dbo.Role"')
GO
CREATE TABLE dbo.Role (
  ID tinyint IDENTITY,
  Name varchar(50) NULL,
  CONSTRAINT PK_Role_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Hotel"
--
PRINT (N'Create table "dbo.Hotel"')
GO
CREATE TABLE dbo.Hotel (
  ID int IDENTITY,
  Name varchar(50) NULL,
  CONSTRAINT PK_Hotel_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Destination"
--
PRINT (N'Create table "dbo.Destination"')
GO
CREATE TABLE dbo.Destination (
  ID int IDENTITY,
  Name varchar(50) NULL,
  CONSTRAINT PK_Destination_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.DatePattern"
--
PRINT (N'Create table "dbo.DatePattern"')
GO
CREATE TABLE dbo.DatePattern (
  ID int IDENTITY,
  Name varchar(50) NULL,
  [From] datetime NULL,
  [to] datetime NULL,
  CONSTRAINT PK_DatePattern_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Coppon"
--
PRINT (N'Create table "dbo.Coppon"')
GO
CREATE TABLE dbo.Coppon (
  ID bigint IDENTITY,
  Code varchar(50) NULL,
  TourID bigint NULL,
  PresenterID int NULL,
  Price money NULL,
  Capacity int NULL,
  HotelID int NULL,
  Parent bigint NULL,
  CONSTRAINT PK_Coppon_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.PriceDetail"
--
PRINT (N'Create table "dbo.PriceDetail"')
GO
CREATE TABLE dbo.PriceDetail (
  ID int IDENTITY,
  Price money NULL,
  UnitID int NULL,
  CONSTRAINT PK_PriceDetail_ID PRIMARY KEY CLUSTERED (ID)
)
ON [PRIMARY]
GO
-- 
-- Dumping data for table Coppon
--
-- Table Tourine.dbo.Coppon does not contain any data (it is empty)
-- 
-- Dumping data for table DatePattern
--
-- Table Tourine.dbo.DatePattern does not contain any data (it is empty)
-- 
-- Dumping data for table Destination
--
-- Table Tourine.dbo.Destination does not contain any data (it is empty)
-- 
-- Dumping data for table Hotel
--
-- Table Tourine.dbo.Hotel does not contain any data (it is empty)
-- 
-- Dumping data for table PriceDetail
--
-- Table Tourine.dbo.PriceDetail does not contain any data (it is empty)
-- 
-- Dumping data for table Role
--
-- Table Tourine.dbo.Role does not contain any data (it is empty)
-- 
-- Dumping data for table Status
--
-- Table Tourine.dbo.Status does not contain any data (it is empty)
-- 
-- Dumping data for table Tour
--
-- Table Tourine.dbo.Tour does not contain any data (it is empty)
-- 
-- Dumping data for table Unit
--
-- Table Tourine.dbo.Unit does not contain any data (it is empty)
-- 
-- Dumping data for table [User]
--
-- Table Tourine.dbo.[User] does not contain any data (it is empty)

USE Tourine
GO

IF DB_NAME() <> N'Tourine' SET NOEXEC ON
GO

--
-- Create foreign key "FK_PriceDetail_Unit_ID" on table "dbo.PriceDetail"
--
PRINT (N'Create foreign key "FK_PriceDetail_Unit_ID" on table "dbo.PriceDetail"')
GO
ALTER TABLE dbo.PriceDetail
  ADD CONSTRAINT FK_PriceDetail_Unit_ID FOREIGN KEY (UnitID) REFERENCES dbo.Unit (ID)
GO

--
-- Create foreign key "FK_User_Role_ID" on table "dbo.[User]"
--
PRINT (N'Create foreign key "FK_User_Role_ID" on table "dbo.[User]"')
GO
ALTER TABLE dbo.[User]
  ADD CONSTRAINT FK_User_Role_ID FOREIGN KEY (RoleID) REFERENCES dbo.Role (ID)
GO

--
-- Create foreign key "FK_Tour_DatePattern_ID" on table "dbo.Tour"
--
PRINT (N'Create foreign key "FK_Tour_DatePattern_ID" on table "dbo.Tour"')
GO
ALTER TABLE dbo.Tour
  ADD CONSTRAINT FK_Tour_DatePattern_ID FOREIGN KEY (DatePatternID) REFERENCES dbo.DatePattern (ID)
GO

--
-- Create foreign key "FK_Tour_Destination_ID" on table "dbo.Tour"
--
PRINT (N'Create foreign key "FK_Tour_Destination_ID" on table "dbo.Tour"')
GO
ALTER TABLE dbo.Tour
  ADD CONSTRAINT FK_Tour_Destination_ID FOREIGN KEY (DestinationID) REFERENCES dbo.Destination (ID)
GO

--
-- Create foreign key "FK_Tour_PriceDetail_ID" on table "dbo.Tour"
--
PRINT (N'Create foreign key "FK_Tour_PriceDetail_ID" on table "dbo.Tour"')
GO
ALTER TABLE dbo.Tour
  ADD CONSTRAINT FK_Tour_PriceDetail_ID FOREIGN KEY (PriceDetailID) REFERENCES dbo.PriceDetail (ID)
GO

--
-- Create foreign key "FK_Tour_Status_ID" on table "dbo.Tour"
--
PRINT (N'Create foreign key "FK_Tour_Status_ID" on table "dbo.Tour"')
GO
ALTER TABLE dbo.Tour
  ADD CONSTRAINT FK_Tour_Status_ID FOREIGN KEY (StatusID) REFERENCES dbo.Status (ID)
GO

--
-- Create foreign key "FK_Coppon_Coppon_ID" on table "dbo.Coppon"
--
PRINT (N'Create foreign key "FK_Coppon_Coppon_ID" on table "dbo.Coppon"')
GO
ALTER TABLE dbo.Coppon WITH CHECK
  ADD CONSTRAINT FK_Coppon_Coppon_ID FOREIGN KEY (Parent) REFERENCES dbo.Coppon (ID)
GO

--
-- Create foreign key "FK_Coppon_Hotel_ID" on table "dbo.Coppon"
--
PRINT (N'Create foreign key "FK_Coppon_Hotel_ID" on table "dbo.Coppon"')
GO
ALTER TABLE dbo.Coppon
  ADD CONSTRAINT FK_Coppon_Hotel_ID FOREIGN KEY (HotelID) REFERENCES dbo.Hotel (ID)
GO

--
-- Create foreign key "FK_Coppon_Tour_ID" on table "dbo.Coppon"
--
PRINT (N'Create foreign key "FK_Coppon_Tour_ID" on table "dbo.Coppon"')
GO
ALTER TABLE dbo.Coppon
  ADD CONSTRAINT FK_Coppon_Tour_ID FOREIGN KEY (TourID) REFERENCES dbo.Tour (ID)
GO

--
-- Create foreign key "FK_Coppon_User_ID" on table "dbo.Coppon"
--
PRINT (N'Create foreign key "FK_Coppon_User_ID" on table "dbo.Coppon"')
GO
ALTER TABLE dbo.Coppon
  ADD CONSTRAINT FK_Coppon_User_ID FOREIGN KEY (PresenterID) REFERENCES dbo.[User] (ID)
GO
SET NOEXEC OFF
GO