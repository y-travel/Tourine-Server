-- <Migration ID="58f7141e-01fe-40f0-899d-95b7d0517ce7" />
GO

PRINT N'Adding constraints to [dbo].[PassengerList]'
GO
ALTER TABLE [dbo].[PassengerList] ADD CONSTRAINT [UC_PassengerList_PersonId_TourId] UNIQUE NONCLUSTERED  ([TourId], [PersonId])
GO

