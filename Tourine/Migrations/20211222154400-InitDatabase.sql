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
-- Create table "dbo.Person"
--
PRINT (N'Create table "dbo.Person"')
GO
CREATE TABLE dbo.Person (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_Person_Id DEFAULT (newid()) ROWGUIDCOL,
  Name nvarchar(50) NOT NULL,
  Family nvarchar(50) NOT NULL,
  MobileNumber varchar(11) NULL,
  NationalCode varchar(10) NULL,
  BirthDate date NULL,
  PassportExpireDate date NULL,
  PassportNo varchar(50) NULL,
  Gender bit NOT NULL,
  Type tinyint NOT NULL,
  SocialNumber varchar(15) NULL,
  ChatId bigint NULL,
  IsUnder5 bit NULL,
  IsInfant bit NULL,
  VisaExpireDate date NULL,
  EnglishName varchar(50) NULL,
  EnglishFamily varchar(50) NULL,
  CONSTRAINT PK_Person_Id PRIMARY KEY CLUSTERED (Id)
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
  PersonId uniqueidentifier NOT NULL,
  Role tinyint NOT NULL,
  CONSTRAINT PK_User_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_User_Person_Id FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Destination"
--
PRINT (N'Create table "dbo.Destination"')
GO
CREATE TABLE dbo.Destination (
  Id uniqueidentifier NOT NULL CONSTRAINT DF__Destination__Id__2A4B4B5E DEFAULT (newid()),
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
  LeaderId uniqueidentifier NULL,
  CONSTRAINT PK_TourDetail_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_TourDetail_Destination_Id FOREIGN KEY (DestinationId) REFERENCES dbo.Destination (Id),
  CONSTRAINT FK_TourDetail_Person_Id FOREIGN KEY (LeaderId) REFERENCES dbo.Person (Id),
  CONSTRAINT FK_TourDetail_Place_Id FOREIGN KEY (PlaceId) REFERENCES dbo.Place (Id)
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
  InfantPrice bigint NULL,
  CreationDate datetime NULL,
  FreeSpace int NULL,
  CONSTRAINT PK_Tour_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_Tour_Agency_Id FOREIGN KEY (AgencyId) REFERENCES dbo.Agency (Id),
  CONSTRAINT FK_Tour_TourDetail_Id FOREIGN KEY (TourDetailId) REFERENCES dbo.TourDetail (Id) ON DELETE CASCADE
)
ON [PRIMARY]
GO

--
-- Create foreign key "FK_Tour_Tour_Id" on table "dbo.Tour"
--
PRINT (N'Create foreign key "FK_Tour_Tour_Id" on table "dbo.Tour"')
GO
ALTER TABLE dbo.Tour WITH CHECK
  ADD CONSTRAINT FK_Tour_Tour_Id FOREIGN KEY (ParentId) REFERENCES dbo.Tour (Id)
GO

--
-- Create table "dbo.TourOption"
--
PRINT (N'Create table "dbo.TourOption"')
GO
CREATE TABLE dbo.TourOption (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_Option_Id DEFAULT (newid()) ROWGUIDCOL,
  OptionType tinyint NULL,
  Price bigint NULL,
  OptionStatus tinyint NULL,
  TourId uniqueidentifier NOT NULL,
  CONSTRAINT PK_Option_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_Option_Tour FOREIGN KEY (TourId) REFERENCES dbo.Tour (Id) ON DELETE CASCADE
)
ON [PRIMARY]
GO

--
-- Create table "dbo.Team"
--
PRINT (N'Create table "dbo.Team"')
GO
CREATE TABLE dbo.Team (
  Id uniqueidentifier NOT NULL CONSTRAINT DF__Team__Id__440B1D61 DEFAULT (newid()),
  TourId uniqueidentifier NOT NULL,
  BuyerId uniqueidentifier NOT NULL,
  Count int NULL,
  SubmitDate datetime NOT NULL,
  InfantPrice bigint NULL,
  BasePrice bigint NULL,
  TotalPrice bigint NULL,
  BuyerIsPassenger bit NULL,
  IsPending bit NULL,
  CONSTRAINT PK_Team_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_Team_Person_Id FOREIGN KEY (BuyerId) REFERENCES dbo.Person (Id),
  CONSTRAINT FK_Team_Tour_Id FOREIGN KEY (TourId) REFERENCES dbo.Tour (Id) ON DELETE CASCADE
)
ON [PRIMARY]
GO

--
-- Create table "dbo.PassengerList"
--
PRINT (N'Create table "dbo.PassengerList"')
GO
CREATE TABLE dbo.PassengerList (
  Id uniqueidentifier NOT NULL CONSTRAINT DF__PassengerList__Id__4D94879B DEFAULT (newid()),
  PersonId uniqueidentifier NOT NULL,
  TourId uniqueidentifier NOT NULL,
  OptionType bigint NOT NULL,
  PassportDelivered bit NULL,
  TeamId uniqueidentifier NULL,
  HaveVisa bit NULL,
  CONSTRAINT PK_PassengerList_Id PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT UC_PassengerList_PersonId_TourId UNIQUE (TourId, PersonId),
  CONSTRAINT FK_PassengerList_Person_Id FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id),
  CONSTRAINT FK_PassengerList_Team_Id FOREIGN KEY (TeamId) REFERENCES dbo.Team (Id) ON DELETE CASCADE,
  CONSTRAINT FK_PassengerList_Tour_Id FOREIGN KEY (TourId) REFERENCES dbo.Tour (Id)
)
ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER, ANSI_NULLS ON
GO

--
-- Create procedure "dbo.spUpdateTourFreeSpace"
--
GO
PRINT (N'Create procedure "dbo.spUpdateTourFreeSpace"')
GO
CREATE PROCEDURE dbo.spUpdateTourFreeSpace @tourId UNIQUEIDENTIFIER
AS 
    DECLARE @tourCapacity BIGINT = 0
    DECLARE @subTourCapacity BIGINT = 0
    DECLARE @mainTourRegistered BIGINT = 0
    SELECT @tourCapacity = t.Capacity
    FROM Tour t
    WHERE t.Id = @tourId
    SELECT @subTourCapacity = ISNULL(SUM(t.Capacity), 0)  
    FROM Tour t
    WHERE t.ParentId = @tourId
    
    SELECT @mainTourRegistered = COUNT(DISTINCT pl.PersonId)
    FROM PassengerList pl , Person p , Team t
    WHERE pl.PersonId = p.Id AND pl.TourId = @tourId AND p.IsInfant <> 1 AND pl.TeamId = t.Id AND t.IsPending <> 1

    UPDATE tour SET freeSpace =  @tourCapacity - @subTourCapacity - @mainTourRegistered
    WHERE Id = @tourId
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
  CONSTRAINT FK_PriceDetail_Tour_Id FOREIGN KEY (TourId) REFERENCES dbo.Tour (Id) ON DELETE CASCADE
)
ON [PRIMARY]
GO

--
-- Create table "dbo.AgencyPerson"
--
PRINT (N'Create table "dbo.AgencyPerson"')
GO
CREATE TABLE dbo.AgencyPerson (
  Id uniqueidentifier NOT NULL CONSTRAINT DF_AgencyPerson_Id DEFAULT (newid()) ROWGUIDCOL,
  AgencyId uniqueidentifier NOT NULL,
  PersonId uniqueidentifier NOT NULL,
  CONSTRAINT PK_AgencyPerson PRIMARY KEY CLUSTERED (Id),
  CONSTRAINT FK_AgencyPerson_Agency_Id FOREIGN KEY (AgencyId) REFERENCES dbo.Agency (Id),
  CONSTRAINT FK_AgencyPerson_Person_Id FOREIGN KEY (PersonId) REFERENCES dbo.Person (Id)
)
ON [PRIMARY]
GO

--
-- Create trigger "dbo.trgUpdateTourFreeSpaceOnTour" on table "dbo.Tour"
--
GO
PRINT (N'Create trigger "dbo.trgUpdateTourFreeSpaceOnTour" on table "dbo.Tour"')
GO
CREATE TRIGGER trgUpdateTourFreeSpaceOnTour
ON dbo.Tour
AFTER INSERT, DELETE, UPDATE
AS
  DECLARE @parentTourId UNIQUEIDENTIFIER
  DECLARE @curTourId UNIQUEIDENTIFIER
  DECLARE @loop_flag BIGINT = 1

  IF EXISTS (SELECT
        *
      FROM inserted) --INSERT/UPDATE tour
  BEGIN
    SELECT
      @parentTourId = i.ParentId
     ,@curTourId = i.Id
    FROM INSERTED i
    WHILE @loop_flag >= 0 -- to update current/parent tour
    BEGIN
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @curTourId
    SET @curTourId = @parentTourId
    SET @loop_flag = @loop_flag - 1
    END;
  END

  IF EXISTS (SELECT
        *
      FROM deleted)
    AND NOT EXISTS (SELECT
        *
      FROM inserted)--DELETE tour
  BEGIN
    SELECT
      @parentTourId = d.ParentId
    FROM Deleted d
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @parentTourId
  END
GO

--
-- Create trigger "dbo.trgUpdateTourFreeSpaceOnTeamUpdate" on table "dbo.Team"
--
GO
PRINT (N'Create trigger "dbo.trgUpdateTourFreeSpaceOnTeamUpdate" on table "dbo.Team"')
GO
CREATE TRIGGER trgUpdateTourFreeSpaceOnTeamUpdate
ON dbo.Team
AFTER UPDATE
AS
  DECLARE @curTourId UNIQUEIDENTIFIER
    SELECT @curTourId = i.TourId
    FROM INSERTED i
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @curTourId
GO

--
-- Create trigger "dbo.trgUpdateTourFreeSpaceOnPassengerList" on table "dbo.PassengerList"
--
GO
PRINT (N'Create trigger "dbo.trgUpdateTourFreeSpaceOnPassengerList" on table "dbo.PassengerList"')
GO
CREATE TRIGGER trgUpdateTourFreeSpaceOnPassengerList
ON dbo.PassengerList
AFTER INSERT, DELETE, UPDATE
AS
  DECLARE @curTourId UNIQUEIDENTIFIER

  IF EXISTS (SELECT
        *
      FROM inserted)
    AND NOT EXISTS (SELECT
        *
      FROM deleted) --INSERT passenger
  BEGIN
    SELECT
      @curTourId = i.TourId
    FROM INSERTED i
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @curTourId
  END

  IF EXISTS (SELECT
        *
      FROM inserted)
    AND EXISTS (SELECT
        *
      FROM deleted) --UPDATE passenger
  BEGIN
    SELECT
      @curTourId = i.TourId
    FROM INSERTED i
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @curTourId

    SELECT
      @curTourId = d.TourId
    FROM DELETED d
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @curTourId
  END

  IF EXISTS (SELECT
        *
      FROM deleted)
    AND NOT EXISTS (SELECT
        *
      FROM inserted) --DELETE passenger
  BEGIN
    SELECT
      @curTourId = d.TourId
    FROM DELETED d
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @curTourId
  END
GO

--
-- Create trigger "dbo.trgUpdateTeamCount" on table "dbo.PassengerList"
--
GO
PRINT (N'Create trigger "dbo.trgUpdateTeamCount" on table "dbo.PassengerList"')
GO
CREATE TRIGGER trgUpdateTeamCount
ON dbo.PassengerList
AFTER DELETE
AS
DECLARE @teamId UNIQUEIDENTIFIER
DECLARE @teamCount BIGINT = 0

SELECT @teamId = d.TeamId
FROM DELETED d

  SELECT @teamCount = COUNT(DISTINCT pl.PersonId)
  FROM PassengerList pl
  WHERE pl.TeamId = @teamId

  IF (@teamCount = 0) 
    DELETE FROM Team  WHERE Id = @teamId
  ELSE 
    UPDATE Team SET Count = @teamCount 
    WHERE Id = @teamId
GO
SET NOEXEC OFF
GO