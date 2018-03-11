CREATE TABLE [dbo].[Team]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__Team__Id__440B1D61] DEFAULT (newid()),
[TourId] [uniqueidentifier] NOT NULL,
[BuyerId] [uniqueidentifier] NOT NULL,
[Count] [int] NULL,
[SubmitDate] [datetime] NOT NULL
)
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [PK_Team_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Person_Id] FOREIGN KEY ([BuyerId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[Team] ADD CONSTRAINT [FK_Team_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
