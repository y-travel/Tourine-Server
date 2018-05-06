using System;
using System.Collections.Generic;
using System.Data.SQLite;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyPersons;
using Tourine.ServiceInterfaces.Persons;
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

        private readonly Person _person = new Person { NationalCode = "0123456789", Family = "any", Type = PersonType.Passenger | PersonType.Leader };
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
            var person = (QueryResponse<Person>)MockService.Get(new FindPersonInAgency { Str = "n", AgencyId = _agency.Id });
            person.Results[0].ShouldBeEquivalentTo(_person);
        }

        [Test]
        public void FindPassengerInAgency_should_return_zero_result()
        {
            var item = (QueryResponse<Person>)MockService.Get(new FindPersonInAgency { Str = "p", AgencyId = _agency.Id });
            item.Results.Count.Should().Be(0);
        }

        [Test]
        public void FindPassengerInAgency_should_throw_exception()//Why: the AgencyId is not exist in db
        {
            new Action(() => MockService.Get(new FindPersonInAgency { Str = "r", AgencyId = Guid.NewGuid() }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void GetLeaders_should_return_result()
        {
            var item = (QueryResponse<Person>)MockService.Get(new GetLeaders());
            item.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetCurrentPerson_should_return_result()
        {
            var person = (Person)MockService.Get(new GetCurrentPerson());
            person.Should().NotBeNull();
            person.Id.Should().Be(_testPersonGuid);
            person.Name.Should().Be("testu");
        }

        [Test]
        public void DeleteLeader_should_throw_excetion()//Why: because this PersonId is not exist
        {
            new Action(() => MockService.Delete(new DeleteLeader { Id = Guid.NewGuid() }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void DeleteLeader_should_not_throw_excetion()
        {
            new Action(() => MockService.Delete(new DeleteLeader { Id = _person.Id }))
                .ShouldNotThrow<HttpError>();

            var leader = (QueryResponse<Person>)MockService.Get(new GetPersons { Id = _person.Id });
            leader.Results[0].Type.Should().Be(PersonType.Passenger);
        }

        [Test]
        public void CalculatePersonAge()
        {
            var today = DateTime.Parse("2018-1-2");
            var day = today.Day;
            var infantPersonUpBorder = new Person { BirthDate = DateTime.Parse("2016-1-3") };
            var under5PersonDownBorder = new Person { BirthDate = DateTime.Parse("2016-1-2") };
            var under5PersonUpBorder = new Person { BirthDate = DateTime.Parse("2013-1-3") };
            var adultPersonDownBorder = new Person { BirthDate = DateTime.Parse("2013-1-2") };
            Assert.AreEqual(infantPersonUpBorder.CalculateAge(today), 1);
            Assert.AreEqual(under5PersonDownBorder.CalculateAge(today), 2);
            Assert.AreEqual(under5PersonUpBorder.CalculateAge(today), 4);
            Assert.AreEqual(adultPersonDownBorder.CalculateAge(today), 5);
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
