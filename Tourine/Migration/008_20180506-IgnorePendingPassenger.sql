-- <Migration ID="5c38e7cd-2949-407d-afd8-4192bb85395d" />
GO

PRINT N'Dropping foreign keys from [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] DROP CONSTRAINT [FK_PassengerList_Tour_Id]
GO
ALTER TABLE [dbo].[PassengerList] DROP CONSTRAINT [FK_PassengerList_Person_Id]
GO
PRINT N'Altering [dbo].[spUpdateTourFreeSpace]'
GO
ALTER PROCEDURE [dbo].[spUpdateTourFreeSpace] @tourId UNIQUEIDENTIFIER
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
PRINT N'Creating trigger [dbo].[trgUpdateTourFreeSpaceOnTeamUpdate] on [dbo].[Team]'
GO
CREATE TRIGGER [dbo].[trgUpdateTourFreeSpaceOnTeamUpdate]
ON [dbo].[Team]
AFTER UPDATE
AS
  DECLARE @curTourId UNIQUEIDENTIFIER
    SELECT @curTourId = i.TourId
    FROM INSERTED i
    EXEC Tourine.dbo.spUpdateTourFreeSpace @tourId = @curTourId
GO
PRINT N'Adding foreign keys to [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
