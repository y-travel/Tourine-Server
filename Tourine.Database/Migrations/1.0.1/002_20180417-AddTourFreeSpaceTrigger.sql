-- <Migration ID="ab468072-3ef1-4c20-9da3-4a6efd88aa3d" />
GO

PRINT N'Altering [dbo].[Tour]'
GO
ALTER TABLE [dbo].[Tour] ADD
[FreeSpace] [int] NULL
GO
PRINT N'Creating [dbo].[spUpdateTourFreeSpace]'
GO
CREATE PROCEDURE [dbo].[spUpdateTourFreeSpace] @tourId UNIQUEIDENTIFIER
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
    FROM PassengerList pl
    WHERE pl.TourId = @tourId

    UPDATE tour SET freeSpace =  @tourCapacity - @subTourCapacity - @mainTourRegistered
    WHERE Id = @tourId
GO
PRINT N'Creating trigger [dbo].[trgUpdateTourFreeSpaceOnPassengerList] on [dbo].[PassengerList]'
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
PRINT N'Creating trigger [dbo].[trgUpdateTourFreeSpcaceOnTour] on [dbo].[Tour]'
GO
CREATE TRIGGER [dbo].[trgUpdateTourFreeSpcaceOnTour]
ON [dbo].[Tour]
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
