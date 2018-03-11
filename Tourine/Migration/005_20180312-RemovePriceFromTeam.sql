-- <Migration ID="ed3e0602-87f6-454f-90e1-8d3002b01c80" />
GO

PRINT N'Altering [dbo].[Team]'
GO
ALTER TABLE [dbo].[Team] DROP
COLUMN [MoneyReceived]
GO
PRINT N'Altering [dbo].[PassengerList]'
GO
EXEC sp_rename N'[dbo].[PassengerList].[Type]', N'OptionType', N'COLUMN'
GO
