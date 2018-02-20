using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.OrmLite;
using Telerik.JustMock;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyPersons;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Users;
using AuthProvider = Tourine.Common.AuthProvider;

namespace Tourine.Test.ServerTest
{
    public class AuthProviderTest 
    {
        //        private static readonly Guid TestPersonGuid = Guid.NewGuid();
        //        private static readonly Guid TestUserGuid = Guid.NewGuid();
        //        private static readonly Guid TestAgencyGuid = Guid.NewGuid();

        TestAppHost appHost = Mock.Create<TestAppHost>();

        [SetUp]
        public new void Setup()
        {
            //            Db.DeleteById<User>(TestUserGuid);
            //            Db.Insert(new User { Id = TestUserGuid, Username = "test", Password = "test", PersonId = TestPersonGuid });
            //            Db.Insert(new Person { Id = TestPersonGuid, Name = "Ali", Family = "Mrz" });
            //            Db.Insert(new AgencyPerson { AgencyId = TestAgencyGuid, PersonId = TestPersonGuid });
            //            Db.Insert(new Agency { Id = TestAgencyGuid, Name = "TaHa" });
        }

        [Test,TestCaseSource(nameof(_toBeAuthenticatedDtos))]
        public void should_be_use_authenticate_attribute(string methodName,Type serviceType, Type dtoType)
        {
            var methodInfo = serviceType.GetMethod(methodName, new [] { dtoType });
            var attributes = methodInfo.GetCustomAttributes(typeof(AuthenticateAttribute), true);
            Assert.IsTrue(attributes.Any(), "No AuthorizeAttribute found on {0}", dtoType.Name);
        }

        private static object[][] _toBeAuthenticatedDtos = new[]
        {
            new object[] {"Get",typeof(UserService), typeof(GetUser)},
            new object[] {"Post" , typeof(AgencyPersonService),typeof(AddPersonToAgency) },
        };
    }
}
