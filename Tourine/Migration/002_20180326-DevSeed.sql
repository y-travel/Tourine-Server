﻿--
-- Script was generated by Devart dbForge Studio for SQL Server, Version 5.3.56.0
-- Product home page: http://www.devart.com/dbforge/sql/studio
-- Script date 3/26/2018 6:46:17 PM
-- Server version: 13.00.4001
-- Client version: 
--


SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO
USE Tourine
GO

IF DB_NAME() <> N'Tourine' SET NOEXEC ON
-- 
-- Dumping data for table Agency
--
INSERT dbo.Agency VALUES ('5d0fd190-3c6a-45d9-9987-f698b700cd43', N'طاها', N'09352222289')
GO
-- 
-- Dumping data for table Currency
--
INSERT dbo.Currency VALUES (1, N'Rial', 1)
INSERT dbo.Currency VALUES (2, N'Toman', 10)
INSERT dbo.Currency VALUES (3, N'Dollar', 40000)
GO
-- 
-- Dumping data for table Destination
--
INSERT dbo.Destination VALUES ('bfc8684b-0a99-406e-9c56-00f3398f72bd', N'کربلا - نجف')
GO
-- 
-- Dumping data for table Person
--
INSERT dbo.Person VALUES ('f0905b1f-9221-4e4e-ac60-0617427602e7', N'عزیز', N'وظیفه دل', N'09352222289', N'-7', '1988-08-01', '2018-01-31', N'123456789a', CONVERT(bit, 'True'), 5, N'989352222289', 0, CONVERT(bit, 'False'), CONVERT(bit, 'False'), '2022-08-10', N'Aziz', N'VazifeDel')
INSERT dbo.Person VALUES ('cdaf2353-b68d-4f0f-8056-5e37e51d70aa', N'عماد', N'باقری', N'09353860767', N'-6', '2008-01-17', '2018-01-30', N'123456789a', CONVERT(bit, 'True'), 1, N'989353860767', 0, CONVERT(bit, 'False'), CONVERT(bit, 'False'), '2000-07-01', N'Emad', N'Bagheri')
INSERT dbo.Person VALUES ('cd689574-7a50-448a-8724-688ca5291777', N'علیرضا', N'زنده دل', N'09355113008', N'-5', '1989-07-01', '2018-01-31', N'123456789a', CONVERT(bit, 'False'), 1, N'989355113008', 0, CONVERT(bit, 'False'), CONVERT(bit, 'True'), '2017-07-11', N'Alireza', N'ZendeDel')
INSERT dbo.Person VALUES ('0cdf3854-efa5-4cca-b659-921a9309c60b', N'محمد', N'فرهمند زاد', N'09385015211', N'-4', '1989-09-01', '2018-01-31', N'123456789a', CONVERT(bit, 'True'), 1, N'989385015211', 0, CONVERT(bit, 'True'), CONVERT(bit, 'False'), '2019-07-21', N'Mohammad', N'FarahmandZad')
INSERT dbo.Person VALUES ('5f05a5dc-3aee-4572-beba-a7d5a5fc3e64', N'مجید', N'شاملو', N'09192480767', N'-3', '1988-01-08', '2018-03-08', N'123456789a', CONVERT(bit, 'True'), 1, N'989192480767', 0, CONVERT(bit, 'False'), CONVERT(bit, 'False'), '2017-07-08', N'Majid', N'Shamlooei')
INSERT dbo.Person VALUES ('070d5d34-2723-4e48-bfe4-b07838e480f1', N'علی', N'میرزایی', N'09125412164', N'-2', '1989-08-01', '2018-01-31', N'123456789a', CONVERT(bit, 'True'), 1, N'989125412164', 0, CONVERT(bit, 'False'), CONVERT(bit, 'False'), '2018-01-01', N'Ali', N'Mirzaei')
INSERT dbo.Person VALUES ('e2d864ca-19eb-40d6-adea-f5bfc7989846', N'سعید', N'شاملو', N'09217322164', N'-1', '1989-08-01', '2018-01-31', N'123456789a', CONVERT(bit, 'False'), 1, N'989217322164', 0, CONVERT(bit, 'True'), CONVERT(bit, 'False'), '2020-07-01', N'Saeid', N'Shamlooei')
GO
-- 
-- Dumping data for table Place
--
INSERT dbo.Place VALUES ('9a6822c4-3f1d-44ef-98e7-56f40c37ff33', N'قصرالعباس - المیزان')
INSERT dbo.Place VALUES ('834c9273-52fd-4b84-94c5-638d883b1ce4', N'قصرالعباس - سلطان')
GO
-- 
-- Dumping data for table [User]
--
INSERT dbo.[User] VALUES ('01455fa2-4171-4db4-9a8e-3a0ecf3156c2', N'Emad', N'1234', 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa', 1)
INSERT dbo.[User] VALUES ('9fbd8acb-029f-4fdf-8130-3a644c82018b', N'Alirzl', N'1234', 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa', 7)
INSERT dbo.[User] VALUES ('fd6ec66b-68ba-4a90-a6f5-48330e35f3dc', N'Ali', N'1234', '070d5d34-2723-4e48-bfe4-b07838e480f1', 255)
INSERT dbo.[User] VALUES ('c84b8e27-fdfd-4753-8cb3-715390048432', N'Saeid', N'1234', 'e2d864ca-19eb-40d6-adea-f5bfc7989846', 2)
INSERT dbo.[User] VALUES ('ef1d50dd-3376-4d84-9821-991734723133', N'Majid', N'1234', '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64', 4)
INSERT dbo.[User] VALUES ('b88bc928-7330-4120-bac8-cc7ff29ba85c', N'Mohammad', N'1234', '0cdf3854-efa5-4cca-b659-921a9309c60b', 255)
INSERT dbo.[User] VALUES ('fdc52981-fac4-48a7-9aa4-f0f7b0835ccb', N'Aziz', N'1234', 'f0905b1f-9221-4e4e-ac60-0617427602e7', 255)
GO
-- 
-- Dumping data for table AgencyPerson
--
INSERT dbo.AgencyPerson VALUES ('8fc522c2-f460-4887-9f31-506d5997e0aa', '5d0fd190-3c6a-45d9-9987-f698b700cd43', 'cd689574-7a50-448a-8724-688ca5291777')
INSERT dbo.AgencyPerson VALUES ('a462cdb7-9d6d-4e94-a9e7-514658640945', '5d0fd190-3c6a-45d9-9987-f698b700cd43', 'f0905b1f-9221-4e4e-ac60-0617427602e7')
INSERT dbo.AgencyPerson VALUES ('e0315517-2618-4b0b-af21-a41c33fcd4ce', '5d0fd190-3c6a-45d9-9987-f698b700cd43', '070d5d34-2723-4e48-bfe4-b07838e480f1')
INSERT dbo.AgencyPerson VALUES ('98dd03e3-e4e2-4339-9c5f-cba83a5c7e9b', '5d0fd190-3c6a-45d9-9987-f698b700cd43', '0cdf3854-efa5-4cca-b659-921a9309c60b')
GO
-- 
-- Dumping data for table TourDetail
--
-- Table Tourine.dbo.TourDetail does not contain any data (it is empty)
-- 
-- Dumping data for table Tour
--
-- Table Tourine.dbo.Tour does not contain any data (it is empty)
-- 
-- Dumping data for table PriceDetail
--
-- Table Tourine.dbo.PriceDetail does not contain any data (it is empty)
-- 
-- Dumping data for table Team
--
-- Table Tourine.dbo.Team does not contain any data (it is empty)
-- 
-- Dumping data for table TourOption
--
-- Table Tourine.dbo.TourOption does not contain any data (it is empty)
-- 
-- Dumping data for table PassengerList
--
-- Table Tourine.dbo.PassengerList does not contain any data (it is empty)
SET NOEXEC OFF
GO