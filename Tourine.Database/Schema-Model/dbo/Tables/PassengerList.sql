CREATE TABLE [dbo].[PassengerList]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PassengerList__Id__4D94879B] DEFAULT (newid()),
[PersonId] [uniqueidentifier] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[OptionType] [bigint] NOT NULL,
[PassportDelivered] [bit] NULL,
[TeamId] [uniqueidentifier] NULL,
[HaveVisa] [bit] NULL
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE TRIGGER [dbo].[trgUpdateTeamCount]
ON [dbo].[PassengerList]
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
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE TRIGGER [dbo].[trgUpdateTourFreeSpaceOnPassengerList]
ON [dbo].[PassengerList]
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
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [PK_PassengerList_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Team_Id] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
