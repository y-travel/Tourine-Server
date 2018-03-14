-- <Migration ID="1c396caf-a99f-4892-a311-8cf3da927199" />
GO

PRINT N'Altering [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] ADD
[VisaExpireDate] [date] NULL
GO
PRINT N'Altering [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD
[PassportDelivered] [bit] NULL,
[VisaDelivered] [bit] NULL
GO
