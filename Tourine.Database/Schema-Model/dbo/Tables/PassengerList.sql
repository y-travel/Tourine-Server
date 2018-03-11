CREATE TABLE [dbo].[PassengerList]
(
[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF__PassengerList__Id__4D94879B] DEFAULT (newid()),
[PersonId] [uniqueidentifier] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[OptionType] [bigint] NOT NULL,
[IncomeStatus] [tinyint] NOT NULL,
[ReceivedMoney] [bigint] NOT NULL,
[CurrencyFactor] [float] NOT NULL
)
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [PK_PassengerList_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Person_Id] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [FK_PassengerList_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id])
GO
