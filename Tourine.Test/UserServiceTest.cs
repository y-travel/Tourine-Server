using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models;
using Tourine.Models.DatabaseModels;

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
            Client.Invoking(x => x.Get(new GetUserInfo { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetUserInfo_should_return_UserInfo()
        {
            var userInfo = Client.Get<UserInfo>(new GetUserInfo { Id = _testUserId });
            userInfo.Should().NotBeNull();
            userInfo.Role.Should().NotBeNull();
        }

        public void CreateUsers()
        {
            Db.Insert(new Role { Id = 1, Name = "Admin" });
            Db.Insert(new User { Id = _testUserId, Name = "Ali", RoleId = 1 });
        }
    }
}
