using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Messaging.Rcon;
using ServiceStack.OrmLite;
using Tourine.Models;

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
        }

        [Test]
        public void GetCustomer_should_throw_exception()
        {
            Client.Invoking(x => x.Get(new GetCustomer { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutCustomer_should_return_inserted_result()
        {
            var item = Client.Put(new PutCustomer
            {
                Customer = new Customer
                {
                    Id = _testId,
                    Name = "Ali",
                    Family = "Mrz",
                    MobileNumber = "09123456789",
                    PassportNo = "12147123",
                    NationalCode = "0012234567"
                }
            });

            item.Id.Should().Be(_testId);
            item.Name.Should().Be("Ali");
        }

        [Test]
        public void PutCustomer_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new PutCustomer
            {
                Customer = new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "A--",
                    Family = "M--",
                    MobileNumber = "09123456789",
                    PassportNo = "12147123",
                    NationalCode = "0012234567"
                }
            })).ShouldThrow<WebServiceException>();

        }

        [Test]
        public void DeleteCustomer_should_return_result()
        {
            var item = Client.Delete(new DeleteCustomer { Id = _testId });
            item.Id.Should().Be(_testId);
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
                NationalCode = "0012345698",
                PassportNo = "123456789abcdef"
            });
        }
    }
}
