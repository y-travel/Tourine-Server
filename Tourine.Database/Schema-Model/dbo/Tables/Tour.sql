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
[CreationDate] [datetime] NULL,
[FreeSpace] [int] NULL
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
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
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [PK_Tour_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_Agency_Id] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_Tour_Id] FOREIGN KEY ([ParentId]) REFERENCES [dbo].[Tour] ([Id])
GO
ALTER TABLE [dbo].[Tour] ADD CONSTRAINT [FK_Tour_TourDetail_Id] FOREIGN KEY ([TourDetailId]) REFERENCES [dbo].[TourDetail] ([Id]) ON DELETE CASCADE
GO
