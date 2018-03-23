-- <Migration ID="e0601d58-76da-46c7-8d79-6052efcbfc50" />
GO

PRINT N'Altering [dbo].[Person]'
GO
ALTER TABLE [dbo].[Person] ADD
[EnglishName] [varchar] (50) NULL,
[EnglishFamily] [varchar] (50) NULL
GO

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO
USE Tourine
GO

IF DB_NAME() <> N'Tourine' SET NOEXEC ON
-- 
-- Dumping data for table Person
--
UPDATE dbo.Person SET EnglishName = 'Aziz' , EnglishFamily = 'VazifeDel' , VisaExpireDate = '2022-08-10' WHERE Id = 'f0905b1f-9221-4e4e-ac60-0617427602e7'
UPDATE dbo.Person SET EnglishName = 'Emad' , EnglishFamily = 'Bagheri'  , VisaExpireDate = '2000-07-01'WHERE Id = 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa'
UPDATE dbo.Person SET EnglishName = 'Alireza' , EnglishFamily = 'ZendeDel'  , VisaExpireDate = '2017-07-11'WHERE Id = 'cd689574-7a50-448a-8724-688ca5291777'
UPDATE dbo.Person SET EnglishName = 'Mohammad' , EnglishFamily = 'FarahmandZad'  , VisaExpireDate = '2019-07-21'WHERE Id = '0cdf3854-efa5-4cca-b659-921a9309c60b'
UPDATE dbo.Person SET EnglishName = 'Majid' , EnglishFamily = 'Shamlooei'  , VisaExpireDate = '2017-07-08'WHERE Id = '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64'
UPDATE dbo.Person SET EnglishName = 'Ali' , EnglishFamily = 'Mirzaei'  , VisaExpireDate = '2018-01-01'WHERE Id = '070d5d34-2723-4e48-bfe4-b07838e480f1'
UPDATE dbo.Person SET EnglishName = 'Saeid' , EnglishFamily = 'Shamlooei'  , VisaExpireDate = '2020-07-01'WHERE Id = 'e2d864ca-19eb-40d6-adea-f5bfc7989846'
GO
SET NOEXEC OFF
GO
