using System;
using System.Collections.Generic;
using System.Data.SQLite;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyPersons;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;
using Tourine.ServiceInterfaces.Users;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class PersonServiceTest : ServiceTest<PersonService>
    {
        private readonly Guid _testUserGuid = Guid.NewGuid();
        private readonly Guid _testPersonGuid = Guid.NewGuid();
        private readonly Guid _testTeamGuid = Guid.NewGuid();
        private readonly Guid _testTourGuid = Guid.NewGuid();
        private readonly Guid _testAgencyGuid = Guid.NewGuid();

        private readonly Person _person = new Person { NationalCode = "0123456789" , Family = "any"};
        private readonly Agency _agency = new Agency();
        [SetUp]
        public new void Setup()
        {
            CreatePerson();
            //            AppHost.Session = new AuthSession
            //            {
            //                TestMode = true,
            //                User = new User { Id = _testUserGuid ,PersonId = _testPersonGuid , Username = "testu"}
            //            };
        }

        [Test]
        public void GetPersons_should_return_result()
        {
            var res = (QueryResponse<Person>)MockService.Get(new GetPersons { Id = _person.Id });
            res.Results[0].ShouldBeEquivalentTo(_person);
        }

        [Test]
        public void GetPersons_should_return_results()
        {
            var res = (QueryResponse<Person>)MockService.Get(new GetPersons());
            res.Results[0].ShouldBeEquivalentTo(_person);
        }

        [Test]
        public void DeletePerson_should_not_throw_exception()
        {
            new Action(() => MockService.Delete(new DeletePerson { Id = _person.Id }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void DeletePerson_should_throw_exeption()
        {
            new Action(() => MockService.Delete(new DeletePerson { Id = Guid.NewGuid() }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void UpdatePerson_should_not_return_exception()
        {
            new Action(() => MockService.Put(new UpdatePerson { Person = _person }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void UpdatePerson_should_throw_exception()
        {
            _person.Id = Guid.NewGuid();
            new Action(() => MockService.Put(new UpdatePerson { Person = _person }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void CreatePerson_should_return_inserted_object()
        {
            _person.Id = Guid.NewGuid();
            var person = (Person)MockService.Post(new AddNewPerson { Person = _person });
            person.ShouldBeEquivalentTo(_person);
        }

        [Test]
        public void CreatePerson_should_return_exception()
        {
            new Action(() => MockService.Post(new AddNewPerson { Person = _person }))
                .ShouldThrow<SQLiteException>();
        }

        [Test]
        public void FindPersonFromNc_should_retuen_result()
        {
            var item = (Person)MockService.Get(new FindPersonFromNc { NationalCode = "0123456789" });
            item.ShouldBeEquivalentTo(_person);
        }

        [Test]
        public void FindPersonFromNc_should_throw_exception()
        {
            new Action(() => MockService.Get(new FindPersonFromNc { NationalCode = "0" }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void FindPersonInAgency_should_return_result()
        {
            var person = (QueryResponse<Person>)MockService.Get(new FindPersonInAgency {Str = "n", AgencyId = _agency.Id});
            person.Results[0].ShouldBeEquivalentTo(_person);
        }

        [Test]
        public void FindPassengerInAgency_should_return_zero_result()
        {
            var item = Client.Get(new FindPersonInAgency { Str = "p", AgencyId = _testAgencyGuid });
            item.Results.Count.Should().Be(0);
        }

        [Test]
        public void FindPassengerInAgency_should_throw_exception()
        {
            Client.Invoking(p => p.Get(new FindPersonInAgency { Str = "r", AgencyId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetLeaders_should_return_result()
        {
            var item = Client.Get(new GetLeaders());
            item.Results.Count.Should().Be(1);
        }

        [Test]
        public void RegisterPassenger_should_return_result()
        {
            List<Guid> psId = new List<Guid>();
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            psId.Add(id1);
            psId.Add(id2);
            var p1 = new Person { Id = id1, Name = "p1", NationalCode = "123456789", Type = 0, };
            var p2 = new Person { Id = id1, Name = "p1", NationalCode = "123456789", Type = 0, };

            var team = Client.Post(new RegisterPerson
            {
                TourId = _testTourGuid,
                BuyerId = _testPersonGuid,
                PassengersId = psId
            });
            team.BuyerId.Should().Be(_testPersonGuid);
            team.Count.Should().Be(psId.Count + 1);
        }

        [Test]
        public void RegisterPassenger_should_throw_exception()
        {
            List<Guid> psId = new List<Guid>();
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            psId.Add(id1);
            psId.Add(id2);
            var p1 = new Person { Id = id1, Name = "p1", NationalCode = "123456789", Type = 0, };
            var p2 = new Person { Id = id1, Name = "p1", NationalCode = "123456789", Type = 0, };

            Client.Invoking(x => x.Post(new RegisterPerson
            {
                TourId = Guid.NewGuid(),
                BuyerId = _testPersonGuid,
                PassengersId = psId
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetCurrentPerson_should_return_result()
        {
            var person = (Person)MockService.Get(new GetCurrentPerson());
            person.Should().NotBeNull();
            person.Id.Should().Be(_testPersonGuid);
            person.Name.Should().Be("testu");
        }
        public void CreatePerson()
        {
            var _agencyPerson = new AgencyPerson { AgencyId = _agency.Id, PersonId = _person.Id };
            InsertDb(_person);
            InsertDb(_agency);
            InsertDb(_agencyPerson);
            //            Db.Insert(new User
            //            {
            //                Id = _testUserGuid,
            //                PersonId = _testPersonGuid
            //            });
            //            Db.Insert(new Person
            //            {
            //                Id = _testPersonGuid,
            //                Name = "emaN",
            //                Family = "Bghr",
            //                MobileNumber = "09125412168",
            //                NationalCode = "0012234567",
            //                Type = PersonType.Leader
            //            });
            //
            //            Db.Insert(new Team
            //            {
            //                Id = _testTeamGuid,
            //                TourId = _testTourGuid,
            //                Count = 2,
            //                Price = 12,
            //                SubmitDate = DateTime.Now
            //            });
            //
            //            Db.Insert(new TeamPerson
            //            {
            //                Id = Guid.NewGuid(),
            //                PersonId = _testPersonGuid,
            //                TeamId = _testTeamGuid
            //            });
            //
            //
            //            Db.Insert(new Agency
            //            {
            //                Id = _testAgencyGuid,
            //                Name = "Taha",
            //                PhoneNumber = "132456789"
            //            });
            //
            //            Db.Insert(new Tour
            //            {
            //                Id = _testTourGuid,
            //                AgencyId = _testAgencyGuid,
            //                Status = TourStatus.Created,
            //                TourDetailId = Guid.NewGuid()
            //            });
        }
    }
}
