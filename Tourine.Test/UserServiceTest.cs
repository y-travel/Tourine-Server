using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models;
using Tourine.Models.DatabaseModels;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class UserServiceTest : ServiceTest
    {
        private readonly Guid _testUserId = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreateUsers();
        }

        [Test]
        public void GetUserInfo_should_throw_exception()
        {
            Client.Invoking(x => x.Get(new GetUser { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetUser_should_return_User()
        {
            var userInfo = Client.Get<User>(new GetUser { Id = _testUserId });
            userInfo.Should().NotBeNull();
        }

        public void CreateUsers()
        {
            Db.Insert(new User
            {
                Id = _testUserId,
                Username = "Ali",
                Password = "123",
                CustomerId = Guid.NewGuid(),
                Role = Role.Admin
            });
        }
    }
}
