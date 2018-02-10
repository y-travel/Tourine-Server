using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyCustomers;
using Tourine.ServiceInterfaces.Customers;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class AgencyCustomerServiceTest : ServiceTest
    {
        private readonly Guid _testUserId = Guid.NewGuid();
        private readonly Guid _testAgencyGuid = Guid.NewGuid();
        private readonly Guid _testAgencyGuid2 = Guid.NewGuid();
        private readonly Guid _testCustomerGuid = Guid.NewGuid();
        private readonly Guid _testCustomerGuid2 = Guid.NewGuid();
        private readonly Guid _testAgencyCustomerGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateAgencyCustomer();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = _testUserId, CustomerId = _testCustomerGuid },
                Agency = new Agency { Id = _testAgencyGuid }
            };
        }

        [Test]
        public void GetCustomerOfAgency_should_return_result()
        {
            var customers = Client.Get(new GetCustomerOfAgency());
            customers.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetCustomerOfAgency_should_return_result_use_id()
        {
            var customers = Client.Get(new GetCustomerOfAgency { AgencyId = _testAgencyGuid });
            customers.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetCustomerOfAgency_should_throw_exception_use_id()
        {
            Client.Invoking(x => x.Get(new GetCustomerOfAgency { AgencyId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_not_throw_exception_use_session()
        {
            Client.Invoking(x => x.Post(new AddCustomerToAgency { CustomerId = _testCustomerGuid2 }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_throw_exception_use_session()
        {
            Client.Invoking(x => x.Post(new AddCustomerToAgency { CustomerId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_not_throw_exception_use_id()
        {
            Client.Invoking(x => x.Post(new AddCustomerToAgency
            {
                CustomerId = _testCustomerGuid2,
                AgencyId = _testAgencyGuid2
            }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_not_throw_exception_use_session_instof_empty_guid()
        {
            Client.Invoking(x => x.Post(new AddCustomerToAgency
                {
                    CustomerId = _testCustomerGuid2,
                    AgencyId = Guid.Empty
                }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddCustomerAgency_should_throw_exception_use_id()
        {
            Client.Invoking(x => x.Post(new AddCustomerToAgency { CustomerId = _testCustomerGuid2, AgencyId = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void UpdateCustomerToAgency_should_not_throw_exception()
        {
            Client.Invoking(x => x.Put(new UpdateCustomerToAgency
            {
                Id = _testAgencyCustomerGuid,
                AgencyId = _testAgencyGuid,
                CustomerId = _testCustomerGuid2
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
                CustomerId = _testCustomerGuid2
            }))
                .ShouldThrow<WebServiceException>();
        }

        public void CreateAgencyCustomer()
        {
            Db.Insert(new Agency { Id = _testAgencyGuid, Name = "Mahan" });
            Db.Insert(new Agency { Id = _testAgencyGuid2, Name = "Mahan2" });
            Db.Insert(new Customer
            {
                Id = _testCustomerGuid,
                Name = "Asad",
                Family = "Asadi",
                MobileNumber = "0120001234"
            });
            Db.Insert(new AgencyCustomer
            {
                Id = _testAgencyCustomerGuid,
                AgencyId = _testAgencyGuid,
                CustomerId = _testCustomerGuid
            });
            Db.Insert(new Customer
            {
                Id = _testCustomerGuid2,
                Name = "Asad2",
                Family = "Asadi2",
                MobileNumber = "01200012342"
            });
        }
    }
}
