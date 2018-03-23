-- <Migration ID="e0601d58-76da-46c7-8d79-6052efcbfc50" />
--GO

--PRINT N'Altering [dbo].[Person]'
--GO
--ALTER TABLE [dbo].[Person] ADD
--[EnglishName] [varchar] (50) NULL,
--[EnglishFamily] [varchar] (50) NULL
--GO

IF DB_NAME() <> N'Tourine' SET NOEXEC ON
-- 
-- Dumping data for table Person
--
UPDATE dbo.Person SET EnglishName = 'Aziz' , EnglishFamily = 'VazifeDel' WHERE Id = 'f0905b1f-9221-4e4e-ac60-0617427602e7'
UPDATE dbo.Person SET EnglishName = 'Emad' , EnglishFamily = 'Bagheri' WHERE Id = 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa'
UPDATE dbo.Person SET EnglishName = 'Alireza' , EnglishFamily = 'ZendeDel' WHERE Id = 'cd689574-7a50-448a-8724-688ca5291777'
UPDATE dbo.Person SET EnglishName = 'Mohammad' , EnglishFamily = 'FarahmandZad' WHERE Id = '0cdf3854-efa5-4cca-b659-921a9309c60b'
UPDATE dbo.Person SET EnglishName = 'Majid' , EnglishFamily = 'Shamlooei' WHERE Id = '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64'
UPDATE dbo.Person SET EnglishName = 'Ali' , EnglishFamily = 'Mirzaei' WHERE Id = '070d5d34-2723-4e48-bfe4-b07838e480f1'
UPDATE dbo.Person SET EnglishName = 'Saeid' , EnglishFamily = 'Shamlooei' WHERE Id = 'e2d864ca-19eb-40d6-adea-f5bfc7989846'
GO
SET NOEXEC OFF
GO
