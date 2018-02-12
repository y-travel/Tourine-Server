﻿using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Customers;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class UserServiceTest : ServiceTest
    {
        private readonly Guid _testUserId = Guid.NewGuid();
        private readonly Guid _testCustomerGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateUsers();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = _testUserId }
            };
        }

        [Test]
        public void GetUser_should_throw_exception()
        {
            Client.Invoking(x => x.Get(new GetUser { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetUser_should_return_User()
        {
            var userInfo = Client.Get<User>(new GetUser { Id = _testUserId });
            userInfo.Should().NotBeNull();
            userInfo.Roles.Should().Contain(Role.Admin);
            userInfo.Roles.Should().Contain(Role.Agency);
        }

        [Test]
        public void PostUser_should_throw_exception()
        {
            Client.Invoking(x => x.Post(new PostUser
            {
                User = new User
                {
                    CustomerId = Guid.NewGuid(),
                    Username = "",
                    Password = "123456789",
                    Role = Role.Admin
                }
            })).ShouldThrow<WebServiceException>();


        }

        [Test]
        public void PostUser_should_not_throw_exception()
        {
            Client.Invoking(x => x.Post(new PostUser
            {
                User = new User
                {
                    CustomerId = Guid.NewGuid(),
                    Username = "test",
                    Password = "12345678",
                    Role = Role.Admin

                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void PutUser_should_throw_exception()
        {
            Client.Invoking(u => u.Put(new PutUser
            {
                User = new User
                {
                    Id = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    Password = "12346789",
                    Username = "validUserName",
                    Role = Role.Admin
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutUser_should_not_throw_exception()
        {
            Client.Invoking(u => u.Put(new PutUser
            {
                User = new User
                {
                    Id = _testUserId,
                    CustomerId = Guid.NewGuid(),
                    Password = "vaidPass",
                    Username = "validUserName",
                    Role = Role.Admin
                }
            })).ShouldNotThrow<WebServiceException>();
        }
        public void CreateUsers()
        {
            Db.Insert(new User
            {
                Id = _testUserId,
                Username = "aias",
                Password = "pass",
                CustomerId = _testCustomerGuid,
                Role = Role.Admin | Role.Agency
            });
            Db.Insert(new Customer
            {
                Id = _testCustomerGuid,
                Name = "CName",
                Family = "CFamily",
                MobileNumber = "09123456789"
            });
        }
    }
}
