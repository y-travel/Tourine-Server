-- <Migration ID="e8ecaf9e-0c51-4e76-b5dd-20be737c1bad" />
GO

PRINT N'Creating trigger [dbo].[trgUpdateTeamCount] on [dbo].[PassengerList]'
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
