CREATE TABLE [dbo].[Team]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Team__Id__440B1D61] DEFAULT (newid()),
[TourId] [uniqueidentifier] NOT NULL,
[BuyerId] [uniqueidentifier] NOT NULL,
[Count] [int] NULL,
[SubmitDate] [datetime] NOT NULL,
[InfantPrice] [bigint] NULL,
[BasePrice] [bigint] NULL,
[TotalPrice] [bigint] NULL,
[BuyerIsPassenger] [bit] NULL,
[IsPending] [bit] NULL
)
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
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
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [PK_Team_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Person_Id] FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id]) ON DELETE CASCADE
GO
