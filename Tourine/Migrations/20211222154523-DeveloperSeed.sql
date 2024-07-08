SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO
-- 
-- Dumping data for table Agency
--
INSERT dbo.Agency(Id, Name, PhoneNumber) VALUES ('4c5ee6a3-c80c-436c-983c-a72b207a8925', N'Mahan', N'03211111')
INSERT dbo.Agency(Id, Name, PhoneNumber) VALUES ('5d0fd190-3c6a-45d9-9987-f698b700cd43', N'Taha', N'02136521478')
GO
-- 
-- Dumping data for table Currency
--
INSERT dbo.Currency(Id, Name, Factor) VALUES (1, N'Rial', 1)
INSERT dbo.Currency(Id, Name, Factor) VALUES (2, N'Toman', 10)
INSERT dbo.Currency(Id, Name, Factor) VALUES (3, N'Dollar', 40000)
GO
-- 
-- Dumping data for table Destination
--
INSERT dbo.Destination(Id, Name) VALUES ('bfc8684b-0a99-406e-9c56-00f3398f72bd', N'کربلا')
INSERT dbo.Destination(Id, Name) VALUES ('37132bd5-297f-4208-89cb-586e546d1bb7', N'نجف')
INSERT dbo.Destination(Id, Name) VALUES ('a21e41d9-be48-4e53-b6ee-5b55d47cfb33', N'بغداد')
INSERT dbo.Destination(Id, Name) VALUES ('3706966d-6cbd-4cf4-aa87-f080a89e0e55', N'سلیمانیه')
GO
-- 
-- Dumping data for table Person
--
INSERT dbo.Person(Id, Name, Family, MobileNumber, NationalCode, BirthDate, PassportExpireDate, PassportNo, Gender, Type, SocialNumber, ChatId) VALUES ('f0905b1f-9221-4e4e-ac60-0617427602e7', N'عزیز', N'وظیفه دل', N'032156498', N'123678901', '1988-08-01', '2018-01-31', N'1324564', CONVERT(bit, 'True'), 0, N'989352222289', NULL)
INSERT dbo.Person(Id, Name, Family, MobileNumber, NationalCode, BirthDate, PassportExpireDate, PassportNo, Gender, Type, SocialNumber, ChatId) VALUES ('cdaf2353-b68d-4f0f-8056-5e37e51d70aa', N'عماد', N'باقری', N'12315645', N'455642211', '2008-01-17', '2018-01-30', NULL, CONVERT(bit, 'True'), 0, N'989353860767', NULL)
INSERT dbo.Person(Id, Name, Family, MobileNumber, NationalCode, BirthDate, PassportExpireDate, PassportNo, Gender, Type, SocialNumber, ChatId) VALUES ('cd689574-7a50-448a-8724-688ca5291777', N'علیرضا', N'زنده دل', N'032156498', N'123678901', '1989-07-01', '2018-01-31', N'1324564', CONVERT(bit, 'True'), 0, N'989355113008', NULL)
INSERT dbo.Person(Id, Name, Family, MobileNumber, NationalCode, BirthDate, PassportExpireDate, PassportNo, Gender, Type, SocialNumber, ChatId) VALUES ('0cdf3854-efa5-4cca-b659-921a9309c60b', N'محمد', N'فرهمند زاد', N'032156498', N'123678901', '1989-09-01', '2018-01-31', N'1324564', CONVERT(bit, 'True'), 0, N'989385015211', NULL)
INSERT dbo.Person(Id, Name, Family, MobileNumber, NationalCode, BirthDate, PassportExpireDate, PassportNo, Gender, Type, SocialNumber, ChatId) VALUES ('5f05a5dc-3aee-4572-beba-a7d5a5fc3e64', N'مجید', N'شاملو', N'021000000', N'012345678', '1988-01-08', NULL, NULL, CONVERT(bit, 'True'), 1, N'989192480767', NULL)
INSERT dbo.Person(Id, Name, Family, MobileNumber, NationalCode, BirthDate, PassportExpireDate, PassportNo, Gender, Type, SocialNumber, ChatId) VALUES ('070d5d34-2723-4e48-bfe4-b07838e480f1', N'علی', N'میرزایی', N'032156498', N'123678901', '1989-08-01', '2018-01-31', N'1324564', CONVERT(bit, 'True'), 1, N'989125412164', 91847133)
INSERT dbo.Person(Id, Name, Family, MobileNumber, NationalCode, BirthDate, PassportExpireDate, PassportNo, Gender, Type, SocialNumber, ChatId) VALUES ('e2d864ca-19eb-40d6-adea-f5bfc7989846', N'سعید', N'شاملو', N'032156498', N'123678901', '1989-08-01', '2018-01-31', N'1324564', CONVERT(bit, 'True'), 0, N'989195804336', NULL)
GO
-- 
-- Dumping data for table Place
--
INSERT dbo.Place(Id, Name) VALUES ('9a6822c4-3f1d-44ef-98e7-56f40c37ff33', N'السلطان')
INSERT dbo.Place(Id, Name) VALUES ('834c9273-52fd-4b84-94c5-638d883b1ce4', N'الفندق')
INSERT dbo.Place(Id, Name) VALUES ('18d084c4-6192-4d8e-a5c2-70302884aed2', N'جواد')
INSERT dbo.Place(Id, Name) VALUES ('dc07d0ae-2a1e-43d4-9b3b-e7cdb2d6b98e', N'مشکوکات')
GO
-- 
-- Dumping data for table [User]
--
INSERT dbo.[User](Id, Username, Password, PersonId, Role) VALUES ('01455fa2-4171-4db4-9a8e-3a0ecf3156c2', N'Emad', N'1234', 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa', 4)
INSERT dbo.[User](Id, Username, Password, PersonId, Role) VALUES ('9fbd8acb-029f-4fdf-8130-3a644c82018b', N'Alirzl', N'1234', 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa', 127)
INSERT dbo.[User](Id, Username, Password, PersonId, Role) VALUES ('fd6ec66b-68ba-4a90-a6f5-48330e35f3dc', N'zoz.zozm', N'1234', '070d5d34-2723-4e48-bfe4-b07838e480f1', 255)
INSERT dbo.[User](Id, Username, Password, PersonId, Role) VALUES ('c84b8e27-fdfd-4753-8cb3-715390048432', N'Saeid', N'1234', 'e2d864ca-19eb-40d6-adea-f5bfc7989846', 4)
INSERT dbo.[User](Id, Username, Password, PersonId, Role) VALUES ('ef1d50dd-3376-4d84-9821-991734723133', N'Majid', N'1234', '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64', 4)
INSERT dbo.[User](Id, Username, Password, PersonId, Role) VALUES ('b88bc928-7330-4120-bac8-cc7ff29ba85c', N'Happiness', N'1234', '0cdf3854-efa5-4cca-b659-921a9309c60b', 127)
INSERT dbo.[User](Id, Username, Password, PersonId, Role) VALUES ('fdc52981-fac4-48a7-9aa4-f0f7b0835ccb', N'Aziz', N'1234', 'f0905b1f-9221-4e4e-ac60-0617427602e7', 64)
GO
-- 
-- Dumping data for table AgencyPerson
--
INSERT dbo.AgencyPerson(Id, AgencyId, PersonId) VALUES ('8fc522c2-f460-4887-9f31-506d5997e0aa', '5d0fd190-3c6a-45d9-9987-f698b700cd43', 'cd689574-7a50-448a-8724-688ca5291777')
INSERT dbo.AgencyPerson(Id, AgencyId, PersonId) VALUES ('a462cdb7-9d6d-4e94-a9e7-514658640945', '5d0fd190-3c6a-45d9-9987-f698b700cd43', 'f0905b1f-9221-4e4e-ac60-0617427602e7')
INSERT dbo.AgencyPerson(Id, AgencyId, PersonId) VALUES ('bb4685bf-94e9-482e-a1e4-9bb4e2dd594f', '4c5ee6a3-c80c-436c-983c-a72b207a8925', 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa')
INSERT dbo.AgencyPerson(Id, AgencyId, PersonId) VALUES ('ec5af7c8-c35f-4e4a-aa84-d5abed44a760', '4c5ee6a3-c80c-436c-983c-a72b207a8925', '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64')
GO
-- 
-- Dumping data for table TourDetail
--
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('d93bae8e-ad1b-4bb6-9d99-43089ce9d3e0', 'a21e41d9-be48-4e53-b6ee-5b55d47cfb33', 5, '2018-02-01 18:20:35.303', '9a6822c4-3f1d-44ef-98e7-56f40c37ff33', CONVERT(bit, 'False'), NULL)
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('8a54103e-7655-428e-b547-4fd190506f7d', 'bfc8684b-0a99-406e-9c56-00f3398f72bd', 10, '2018-02-02 18:19:20.873', '834c9273-52fd-4b84-94c5-638d883b1ce4', CONVERT(bit, 'False'), '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64')
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('4beef3e3-c112-4a90-a0c3-651858f32195', '37132bd5-297f-4208-89cb-586e546d1bb7', 10, '2018-02-03 18:20:01.817', '834c9273-52fd-4b84-94c5-638d883b1ce4', NULL,  '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64')
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('ed64d601-6557-4263-b936-8dc57129e453', 'a21e41d9-be48-4e53-b6ee-5b55d47cfb33', 5, '2018-02-04 18:20:35.303', '9a6822c4-3f1d-44ef-98e7-56f40c37ff33', CONVERT(bit, 'False'), NULL)
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('2e4b0d34-64b8-4b9a-9ff3-c31a09a9680b', 'a21e41d9-be48-4e53-b6ee-5b55d47cfb33', 5, '2018-02-05 18:20:35.303', '9a6822c4-3f1d-44ef-98e7-56f40c37ff33', NULL, NULL)
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('c31294e5-cce3-4ece-b2ac-cfa159611f76', 'a21e41d9-be48-4e53-b6ee-5b55d47cfb33', 0, '2018-01-06 18:20:47.000', '9a6822c4-3f1d-44ef-98e7-56f40c37ff33', CONVERT(bit, 'False'), NULL)
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('7136e265-b3d3-4912-86c7-f58269d16d91', 'a21e41d9-be48-4e53-b6ee-5b55d47cfb33', 5, '2018-02-07 18:20:35.303', '9a6822c4-3f1d-44ef-98e7-56f40c37ff33', CONVERT(bit, 'False'), NULL)
INSERT dbo.TourDetail(Id, DestinationId, Duration, StartDate, PlaceId, IsFlight,  LeaderId) VALUES ('e95a0165-0418-4393-920f-fbb68ec696cb', 'a21e41d9-be48-4e53-b6ee-5b55d47cfb33', 5, '2018-02-08 18:20:35.303', '9a6822c4-3f1d-44ef-98e7-56f40c37ff33', CONVERT(bit, 'False'), NULL)
GO
-- 
-- Dumping data for table Tour
--
INSERT dbo.Tour(Id, Capacity, BasePrice, ParentId, Code, Status, TourDetailId, AgencyId) VALUES ('c17496cf-7a71-451f-91da-1d10b165be13', 20, 1600000, NULL, N'bagh20180131', 1, '8a54103e-7655-428e-b547-4fd190506f7d', '5d0fd190-3c6a-45d9-9987-f698b700cd43')
INSERT dbo.Tour(Id, Capacity, BasePrice, ParentId, Code, Status, TourDetailId, AgencyId) VALUES ('443237f8-7eec-413f-9858-97bfd99ebc2b', 50, 750000, 'c17496cf-7a71-451f-91da-1d10b165be13', N'kar20180131', 1, '4beef3e3-c112-4a90-a0c3-651858f32195', '5d0fd190-3c6a-45d9-9987-f698b700cd43')
INSERT dbo.Tour(Id, Capacity, BasePrice, ParentId, Code, Status, TourDetailId, AgencyId) VALUES ('30a9c61b-9dde-4c3d-b681-9ecb1bafedf1', 50, 850000, NULL, N'naj20180113', 1, '4beef3e3-c112-4a90-a0c3-651858f32195', '5d0fd190-3c6a-45d9-9987-f698b700cd43')
GO
-- 
-- Dumping data for table PriceDetail
--
INSERT dbo.PriceDetail(Id, Value, CurrencyId, TourId, Title) VALUES ('6fa90ed7-c05c-4a4b-83de-3bfccf417d19', 60000, 2, '30a9c61b-9dde-4c3d-b681-9ecb1bafedf1', N'mechanic')
INSERT dbo.PriceDetail(Id, Value, CurrencyId, TourId, Title) VALUES ('5f81088d-422f-4d82-824c-80ef6994fcec', 120000, 2, 'c17496cf-7a71-451f-91da-1d10b165be13', N'no')
GO
-- 
-- Dumping data for table Team
--
INSERT dbo.Team(Id, TourId, BuyerId, Count, SubmitDate) VALUES ('7e33370f-6ee0-4297-acd0-237783d4e4f1', '443237f8-7eec-413f-9858-97bfd99ebc2b', 'cdaf2353-b68d-4f0f-8056-5e37e51d70aa', 1, '2018-01-31 18:27:04.893')
INSERT dbo.Team(Id, TourId, BuyerId, Count, SubmitDate) VALUES ('c3fde9b5-234a-4eb8-8c0c-ab48e807ffc7', 'c17496cf-7a71-451f-91da-1d10b165be13',  '5f05a5dc-3aee-4572-beba-a7d5a5fc3e64', 2, '2018-01-31 18:26:45.133')
GO
SET NOEXEC OFF
GO