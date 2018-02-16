using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyCustomers;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class AgencyCustomerServiceTest : ServiceTest
    {
        private readonly Guid _testUserId = Guid.NewGuid();
        private readonly Guid _testAgencyGuid = Guid.NewGuid();
        private readonly Guid _testAgencyGuid2 = Guid.NewGuid();
        private readonly Guid _testPersonGuid = Guid.NewGuid();
        private readonly Guid _testPersonGuid2 = Guid.NewGuid();
        private readonly Guid _testAgencyPersonGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateAgencyCustomer();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = _testUserId, PersonId = _testPersonGuid },
                Agency = new Agency { Id = _testAgencyGuid }
            };
        }

        [Test]
        public void GetCustomerOfAgency_should_return_result()
        {
            var customers = Client.Get(new GetPersonOfAgency());
            customers.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetCustomerOfAgency_should_return_result_use_id()
        {
            var customers = Client.Get(new GetPersonOfAgency { AgencyId = _testAgencyGuid });
            customers.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetCustomerOfAgency_should_throw_exception_use_id()
        {
            Client.Invoking(x => x.Get(new GetPersonOfAgency { AgencyId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_not_throw_exception_use_session()
        {
            Client.Invoking(x => x.Post(new AddPersonToAgency { PersonId = _testPersonGuid2 }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_throw_exception_use_session()
        {
            Client.Invoking(x => x.Post(new AddPersonToAgency { PersonId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_not_throw_exception_use_id()
        {
            Client.Invoking(x => x.Post(new AddPersonToAgency
            {
                PersonId = _testPersonGuid2,
                AgencyId = _testAgencyGuid2
            }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_not_throw_exception_use_session_instof_empty_guid()
        {
            Client.Invoking(x => x.Post(new AddPersonToAgency
                {
                    PersonId = _testPersonGuid2,
                    AgencyId = Guid.Empty
                }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_throw_exception_use_id()
        {
            Client.Invoking(x => x.Post(new AddPersonToAgency { PersonId = _testPersonGuid2, AgencyId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void UpdateCustomerToAgency_should_not_throw_exception()
        {
            Client.Invoking(x => x.Put(new UpdateCustomerToAgency
            {
                Id = _testAgencyPersonGuid,
                AgencyId = _testAgencyGuid,
                PersonId = _testPersonGuid2
            }))
            .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void UpdateCustomerToAgency_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new UpdateCustomerToAgency
            {
                Id = Guid.NewGuid(),
                AgencyId = _testAgencyGuid,
                PersonId = _testPersonGuid2
            }))
                .ShouldThrow<WebServiceException>();
        }

        public void CreateAgencyCustomer()
        {
            Db.Insert(new Agency { Id = _testAgencyGuid, Name = "Mahan" });
            Db.Insert(new Agency { Id = _testAgencyGuid2, Name = "Mahan2" });
            Db.Insert(new Person
            {
                Id = _testPersonGuid,
                Name = "Asad",
                Family = "Asadi",
                MobileNumber = "0120001234"
            });
            Db.Insert(new AgencyPerson
            {
                Id = _testAgencyPersonGuid,
                AgencyId = _testAgencyGuid,
                PersonId = _testPersonGuid
            });
            Db.Insert(new Person
            {
                Id = _testPersonGuid2,
                Name = "Asad2",
                Family = "Asadi2",
                MobileNumber = "01200012342"
            });
        }
    }
}
