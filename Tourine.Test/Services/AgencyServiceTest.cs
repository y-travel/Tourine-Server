using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.FluentValidation.TestHelper;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class AgencyServiceTest : ServiceTest<AgencyService>
    {
        private readonly Guid _testAgencyGuid = Guid.NewGuid();

        private readonly Person _person = new Person();
        private readonly Agency _agency = new Agency();
        private readonly AgencyPerson _agencyPerson = new AgencyPerson();

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            CreateAgency();
        }

        [Test]
        public void GetAgency_should_return_result()
        {
            var item = (Agency)MockService.Get(new GetAgency { Id = _agency.Id });
            item.Id.Should().Be(_agency.Id);
        }

        [Test]
        public void GetAgency_should_throw_exception()
        {
            new Action(() => MockService.Get(new GetAgency { Id = Guid.NewGuid() }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void GetAgencies_should_return_results()
        {
            var results = (QueryResponse<Agency>)MockService.Get(new GetAgencies { IsAll = true });
            results.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetAgencies_should_return_empty()//why: should not return own agency
        {
            var results = (QueryResponse<Agency>)MockService.Get(new GetAgencies { IsAll = false });
            results.Results.Count.Should().Be(0);
        }


        [Test]
        public void UpdateAgency_should_throw_exception()
        {
            _agency.Id = Guid.NewGuid();
            new Action(() => MockService.Put(new UpdateAgency { Agency = _agency }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void UpdateAgency_should_not_throw_exception()
        {

            new Action(() => MockService.Put(new UpdateAgency { Agency = _agency }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void CreateAgency_should_return_inserted_result()
        {
            _agency.Id = Guid.NewGuid();
            _person.Id = Guid.NewGuid();
            var agency = (Agency)MockService.Post(new CreateAgency { Agency = _agency, Person = _person });
            agency.ShouldBeEquivalentTo(_agency);
        }

        private void CreateAgency()
        {
            _agencyPerson.AgencyId = _agency.Id;
            _agencyPerson.PersonId = _person.Id;

            InsertDb(_person);
            InsertDb(new User { PersonId = _person.Id }, true);
            InsertDb(_agency, true);
            InsertDb(_agencyPerson);
        }

    }

    public class AgencyValidationTest
    {
        [Test]
        public void CreateAgencyValidator_should_throw_error()
        {
            var agency = new Agency {Name = "",PhoneNumber = ""};
            var createAgency = new CreateAgency {Agency = agency , Person = new Person()};
            var validator = new CreateAgencyValidator();
            var results = validator.Validate(createAgency);
            Assert.False(results.IsValid);
        }
    }
}
