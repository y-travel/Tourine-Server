using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models.DatabaseModels;
using Tourine.ServiceInterfaces.Passengers;

namespace Tourine.Test
{
    public class PassengerServiceTest : ServiceTest
    {
        private readonly Guid _testGuid = Guid.NewGuid();
        private readonly Guid _testGuidAgency = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreatePassenger();
        }

        [Test]
        public void GetPassenger_should_return_result()
        {
            var res = Client.Get(new GetPassengers());
            res.Results.Count.Should().Be(1);
            res.Results[0].Name.Should().Be("emaN");
        }

        [Test]
        public void DeletePassenger_should_not_throw_exception()
        {
            Client.Invoking(x => x.Delete(new DeletePassenger { Id = _testGuid }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void DeletePassenger_should_throw_exeption()
        {
            Client.Invoking(x => x.Delete(new DeletePassenger { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutPassenger_should_not_return_exception()
        {
            var it = new Passenger
            {
                Id = _testGuid,
                BirthDate = DateTime.Now,
                PassportExpireDate = DateTime.Now,
                NationalCode = "123456",
                PassportNo = "456789",
                AgencyId = _testGuidAgency,
                Family = "Mrz",
                MobileNumber = "45678987",
                Name = "Ali"
            };
            Client.Invoking(x => x.Put(new PutPassenger
            {
                Passenger = it
            })).ShouldNotThrow<WebServiceException>();
            /*            Client.Invoking(x => x.Put(new PutPassenger
                       {
                          Passenger = new Passenger
                           {
                               Id = _testGuid,
                               Name = "emaN",
                               Family = "fdj",
                               AgencyId = Guid.NewGuid(),
                               MobileNumber = "00989125412164",
                               BirthDate = DateTime.Now,
                               PassportExpireDate = DateTime.Now,
                               NationalCode = "123456789",
                               PassportNo = "123456789"
                           }
                       })).ShouldNotThrow<WebServiceException>();*/
        }

        [Test]
        public void PutPassenger_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new PutPassenger
            {
                Passenger = new Passenger
                {
                    Id = Guid.NewGuid(),
                    Name = "emaN",
                    Family = "fdj",
                    AgencyId = Guid.NewGuid(),
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
        public void PostPassenger_should_not_return_exception()
        {

            Client.Invoking(x => x.Post(new PostPassenger
            {
                Passenger = new Passenger
                {
                    Name = "emaN",
                    Family = "fdj",
                    AgencyId = Guid.NewGuid(),
                    MobileNumber = "09125412162"
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        public void CreatePassenger()
        {
            Db.Insert(new Passenger
            {
                Id = _testGuid,
                Name = "emaN",
                Family = "Bghr",
                AgencyId = Guid.NewGuid(),
                MobileNumber = "09125412168"
            });
            Db.Insert(new Agency
            {
                Id = _testGuidAgency,
                Name = "Taha",
                PhoneNumber = "132456789"
            });
        }
    }
}
