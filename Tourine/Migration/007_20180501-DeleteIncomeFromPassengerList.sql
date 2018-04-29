-- <Migration ID="f4c7ec1b-b337-4dc3-bca4-f5465f5abcbd" />
GO

PRINT N'Altering [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] DROP
COLUMN [IncomeStatus],
COLUMN [ReceivedMoney],
COLUMN [CurrencyFactor]
GO