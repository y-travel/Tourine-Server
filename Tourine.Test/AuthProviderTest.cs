using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyCustomers;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class AuthProviderTest : ServiceTest
    {
        private readonly Guid _testCustomerGuid = Guid.NewGuid();
        private readonly Guid _testUserGuid = Guid.NewGuid();
        private readonly Guid _testAgencyGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            Db.Insert(new User { Id = _testUserGuid, Username = "test", Password = "test", PersonId = _testCustomerGuid });
            Db.Insert(new Person { Id = _testCustomerGuid, Name = "Ali", Family = "Mrz" });
            Db.Insert(new AgencyPerson { AgencyId = _testAgencyGuid, PersonId = _testCustomerGuid });
            Db.Insert(new Agency { Id = _testAgencyGuid , Name  = "TaHa"});
        }

        [Test]
        public void try_authenticate_should_return_result()
        {
            Client.Invoking(x => x.Post(new Authenticate { UserName = "test", Password = "test" }))
                .ShouldNotThrow();
        }

        [Test]
        public void GetUser_should_return_result()
        {
            Client.Post(new Authenticate { Password = "test", UserName = "test" });
            Client.Invoking(x => x.Get(new GetUser { Id = _testUserGuid }))
                .ShouldNotThrow();
        }
    }
}
