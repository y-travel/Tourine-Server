using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.FluentValidation.TestHelper;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyPersons;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class AgencyPersonServiceTest : ServiceTest<AgencyPersonService>
    {
        private readonly Person _person = new Person();
        private readonly Agency _agency = new Agency();
        private readonly AgencyPerson _agencyPerson = new AgencyPerson();

        [SetUp]
        public new void Setup()
        {
            CreateAgencyPerson();
        }

        [Test]
        public void GetPersonOfAgency_should_return_result()
        {
            var customers = (QueryResponse<Person>)MockService.Get(new GetPersonOfAgency());
            customers.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetPersonOfAgency_should_return_result_use_id()
        {
            var customers = (QueryResponse<Person>)MockService.Get(new GetPersonOfAgency { AgencyId = _agency.Id });
            customers.Results.Count.Should().Be(1);
        }
         
        [Test]
        public void GetPersonOfAgency_should_throw_exception_use_id()
        {
            new Action(() => MockService.Get(new GetPersonOfAgency { AgencyId = Guid.NewGuid() }))
               .ShouldThrow<HttpError>();
        }

        [Test]
        public void AddPersonAgency_should_not_throw_exception_use_session()
        {
            new Action(() => MockService.Post(new AddPersonToAgency { PersonId = _person.Id }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void AddPersonAgency_should_throw_exception_use_session()
        {
            new Action(() => MockService.Post(new AddPersonToAgency { PersonId = Guid.NewGuid() }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void AddPersonAgency_should_not_throw_exception_use_id()
        {
            new Action(() => MockService.Post(new AddPersonToAgency { PersonId = _person.Id, AgencyId = _agency.Id }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void AddPersonAgency_should_not_throw_exception_use_session_instof_empty_guid()
        {
            new Action(() => MockService.Post(new AddPersonToAgency { PersonId = _person.Id, AgencyId = Guid.Empty }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void AddPersonAgency_should_throw_exception_use_id()
        {
            new Action(() => MockService.Post(new AddPersonToAgency { PersonId = _person.Id, AgencyId = Guid.NewGuid() }))
               .ShouldThrow<HttpError>();
        }

        [Test]
        public void UpdatePersonToAgency_should_not_throw_exception()
        {
            new Action(() => MockService.Put(new UpdatePersonToAgency
            {
                Id = _agencyPerson.Id,
                PersonId = _person.Id,
                AgencyId = _agency.Id
            }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void UpdatePersonToAgency_should_throw_exception()
        {
            new Action(() => MockService.Put(new UpdatePersonToAgency
            {
                Id = Guid.NewGuid(),
                AgencyId = _agency.Id,
                PersonId = _person.Id
            }))
                .ShouldThrow<HttpError>();
        }

        private void CreateAgencyPerson()
        {
            _agencyPerson.AgencyId = _agency.Id;
            _agencyPerson.PersonId = _person.Id;

            InsertDb(_person);
            InsertDb(new User { PersonId = _person.Id }, true);
            InsertDb(_agency, true);
            InsertDb(_agencyPerson);
        }
    }

    public class AgencyPersonValidationTest
    {
        [Test]
        public void addPersonToAgencyValidator_should_throw_error()
        {
            var validator = new AddPersonToAgencyValidator();
            validator.ShouldHaveValidationErrorFor(x => x.AgencyId, Guid.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.AgencyId, (Guid?)null);
            validator.ShouldHaveValidationErrorFor(x => x.PersonId, Guid.Empty);
        }

        [Test]
        public void UpdatePersonToAgency_should_throw_error()
        {
            var validator = new UpdatePersonToAgencyValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Id, Guid.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.PersonId, Guid.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.AgencyId, Guid.Empty);
        }

    }
}
