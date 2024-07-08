using System;
using System.Linq;
using NUnit.Framework;
using ServiceStack;
using Telerik.JustMock;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;
using AuthProvider = Tourine.Common.AuthProvider;

namespace Tourine.Test
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
        public void should_be_use_authenticate_attribute(string methodName,Type optionType, Type dtoType)
        {
            var methodInfo = optionType.GetMethod(methodName, new [] { dtoType });
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
