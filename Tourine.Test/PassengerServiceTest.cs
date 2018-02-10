using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.Test
{
    public class PassengerServiceTest : ServiceTest
    {
        private readonly Guid _testPassengerGuid = Guid.NewGuid();
        private readonly Guid _testTeamGuid = Guid.NewGuid();
        private readonly Guid _testTourGuid = Guid.NewGuid();
        private readonly Guid _testAgencyGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreatePassenger();
            AppHost.Session = new AuthSession { TestMode = true };
        }

        [Test]
        public void GetPassengers_should_return_result()
        {
            var res = Client.Get(new GetPassengers());
            res.Results.Count.Should().Be(1);
            res.Results[0].Name.Should().Be("emaN");
        }

        [Test]
        public void DeletePassenger_should_not_throw_exception()
        {
            Client.Invoking(x => x.Delete(new DeletePassenger { Id = _testPassengerGuid }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void DeletePassenger_should_throw_exeption()
        {
            Client.Invoking(x => x.Delete(new DeletePassenger { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void UpdatePassenger_should_not_return_exception()
        {
            var it = new Passenger
            {
                Id = _testPassengerGuid,
                BirthDate = DateTime.Now,
                PassportExpireDate = DateTime.Now,
                NationalCode = "123456",
                PassportNo = "456789",
                Family = "Mrz",
                MobileNumber = "45678987",
                Name = "Ali"
            };
            Client.Invoking(x => x.Put(new UpdatePassenger
            {
                Passenger = it
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void UpdatePassenger_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new UpdatePassenger
            {
                Passenger = new Passenger
                {
                    Id = Guid.NewGuid(),
                    Name = "emaN",
                    Family = "fdj",
                    MobileNumber = "09125412164",
                    BirthDate = DateTime.Now,
                    NationalCode = "123456789",
                    PassportExpireDate = DateTime.Now,
                    PassportNo = "123456879"
                }
            }))
            .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void CreatePassenger_should_not_return_exception()
        {

            Client.Invoking(x => x.Post(new CreatePassenger
            {
                Passenger = new Passenger
                {
                    Name = "emaN",
                    Family = "fdj",
                    MobileNumber = "09125412162",
                    BirthDate = DateTime.Today,
                    NationalCode = "1234567890",
                    PassportExpireDate = DateTime.MaxValue,
                    PassportNo = "12345689",
                    Gender = true,
                    Type = PassengerType.Passenger
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void CreatePassenger_should_return_exception()
        {

            Client.Invoking(x => x.Post(new CreatePassenger
            {
                Passenger = new Passenger
                {
                    Name = "emaN",
                    Family = "fdj",
                    MobileNumber = "09125412162",
                    BirthDate = DateTime.Today,
                    NationalCode = "1234567890",
                    PassportExpireDate = DateTime.MaxValue
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void FindPassengerFromNc_should_retuen_result()
        {
            var item = Client.Get(new FindPassengerFromNc { NationalCode = "0012234567" });
            item.Name.Should().Be("emaN");
        }

        [Test]
        public void FindPassengerFromNc_should_throw_exception()
        {
            Client.Invoking(x => x.Get(new FindPassengerFromNc { NationalCode = "000000000" }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void FindPassengerInAgency_should_return_result()
        {
            var item = Client.Get(new FindPassengerInAgency { Str = "r", AgencyId = _testAgencyGuid });
            item.Results.Count.Should().Be(1);
        }

        [Test]
        public void FindPassengerInAgency_should_return_zero_result()
        {
            var item = Client.Get(new FindPassengerInAgency { Str = "p", AgencyId = _testAgencyGuid });
            item.Results.Count.Should().Be(0);
        }

        [Test]
        public void FindPassengerInAgency_should_throw_exception()
        {
            Client.Invoking(p => p.Get(new FindPassengerInAgency { Str = "r", AgencyId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetLeaders_should_return_result()
        {
            var item = Client.Get(new GetLeaders());
            item.Results.Count.Should().Be(1);
        }

        public void CreatePassenger()
        {
            Db.Insert(new Passenger
            {
                Id = _testPassengerGuid,
                Name = "emaN",
                Family = "Bghr",
                MobileNumber = "09125412168",
                NationalCode = "0012234567",
                Type = PassengerType.Leader
            });

            Db.Insert(new Team
            {
                Id = _testTeamGuid,
                TourId = _testTourGuid,
                Count = 2,
                Price = 12,
                SubmitDate = DateTime.Now
            });

            Db.Insert(new TeamPassenger
            {
                Id = Guid.NewGuid(),
                PassengerId = _testPassengerGuid,
                TeamId = _testTeamGuid
            });


            Db.Insert(new Agency
            {
                Id = _testAgencyGuid,
                Name = "Taha",
                PhoneNumber = "132456789"
            });

            Db.Insert(new Tour
            {
                Id = _testTourGuid,
                AgencyId = _testAgencyGuid,
                Status = TourStatus.Created,
                TourDetailId = Guid.NewGuid()
            });
        }
    }
}
