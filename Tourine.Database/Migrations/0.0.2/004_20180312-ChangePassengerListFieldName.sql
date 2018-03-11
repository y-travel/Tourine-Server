-- <Migration ID="b8579d67-1347-428c-86ec-d26dfd5f72e2" />
GO

PRINT N'Altering [dbo].[PassengerList]'
GO
EXEC sp_rename N'[dbo].[PassengerList].[Status]', N'IncomeStatus', N'COLUMN'
GO
EXEC sp_rename N'[dbo].[PassengerList].[Price]', N'ReceivedMoney', N'COLUMN'
GO
PRINT N'Altering [dbo].[TourOption]'
GO
EXEC sp_rename N'[dbo].[TourOption].[Status]', N'OptionStatus', N'COLUMN'
GO
