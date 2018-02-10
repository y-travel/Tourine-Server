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
    public class CustomerServiceTest : ServiceTest
    {
        private readonly Guid _testId = Guid.NewGuid();
        private readonly Guid _testUserId = Guid.NewGuid();
        private readonly Guid _testAgencyGuid = Guid.NewGuid();
        private readonly Guid _testCustomerGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateCustomer();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = _testUserId, CustomerId = _testCustomerGuid },
                Agency = new Agency { Id = _testAgencyGuid }
            };

        }

        [Test]
        public void GetCustomers_should_return_result()
        {
            var item = Client.Get(new GetCustomers());
            item.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetCustomer_should_return_result()
        {
            var res = Client.Get(new GetCustomer { Id = _testId });
            res.Should().NotBeNull();
            res.Id.Should().Be(_testId);
        }

        [Test]
        public void GetCustomer_should_throw_exception()
        {
            Client.Invoking(x => x.Get(new GetCustomer { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void UpdateCustomer_should_not_return_exception()
        {
            Client.Invoking(x => x.Put(new UpdateCustomer
            {
                Customer = new Customer
                {
                    Id = _testId,
                    Name = "Ali",
                    Family = "Mrz",
                    MobileNumber = "09123456789",
                    Phone = "09122136458"
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void UpdateCustomer_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new UpdateCustomer
            {
                Customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "A-6",
                    Family = "M-5",
                    MobileNumber = "09123456789",
                    Phone = "123456"
                }
            })).ShouldThrow<WebServiceException>();

        }

        [Test]
        public void CreateCustomer_should_throw_exception()
        {
            Client.Invoking(x => x.Post(new CreateCustomer
            {
                Customer = new Customer
                {
                    Family = "M-5",
                    MobileNumber = "09123456789",
                    Phone = "123456",
                    Name = ""
                }
            })).ShouldThrow<WebServiceException>();

        }

        [Test]
        public void CreateCustomer_should_not_throw_exception()
        {
            Client.Invoking(x => x.Post(new CreateCustomer
            {
                Customer = new Customer
                {
                    Family = "M-5",
                    MobileNumber = "09123456789",
                    Phone = "123456",
                    Name = "123"
                }
            })).ShouldNotThrow<WebServiceException>();

        }

        [Test]
        public void DeleteCustomer_should_not_throw_exception()
        {
            Client.Invoking(x => x.Delete(new DeleteCustomer { Id = _testId }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void DeleteCustomer_should_throw_exception()
        {
            Client.Invoking(x => x.Delete(new DeleteCustomer { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        public void CreateCustomer()
        {
            Db.Insert(new Customer
            {
                Id = _testId,
                Name = "Emad",
                Family = "Bagheri",
                MobileNumber = "09126963724",
                Phone = "09145236987"
            });
            Db.Insert(new AgencyCustomer
            {
                CustomerId = _testId,
                AgencyId = _testAgencyGuid
            });
        }
    }
}
