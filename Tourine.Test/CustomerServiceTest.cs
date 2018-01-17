using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Customers;

namespace Tourine.Test
{
    public class CustomerServiceTest : ServiceTest
    {
        private readonly Guid _testId = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreateCustomer();
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
        public void PutCustomer_should_not_return_exception()
        {
            Client.Invoking(x => x.Put(new PutCustomer
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
        public void PutCustomer_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new PutCustomer
            {
                Customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "A-6",
                    Family = "M-5",
                    MobileNumber = "09123456789"
                }
            })).ShouldThrow<WebServiceException>();

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
        }
    }
}
