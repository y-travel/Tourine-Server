using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models;

namespace Tourine.Test
{
    public class UserServiceTest : ServiceTest
    {
        [SetUp]
        public new void Setup()
        {
            CreateUsers();
        }

        [Test]
        public void GetUser_should_return_result()
        {
            var res = Client.Get(new GetUsers { Name = "Ali"});
            res.Results.Count.Should().Be(1);

        }

        [Test]
        public void GetUser_should_throw_exception()
        {
            Client.Invoking(x => x.Get(new GetUsers()))
                .ShouldThrow<WebServiceException>()
                .Which
                .StatusCode
                .Should()
                .Be(404);
        }

        public void CreateUsers()
        {
            Db.Insert(new User { Name = "Ali" });
        }
    }
}
