using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models;

namespace Tourine.Test
{
    public class TourServiceTest : ServiceTest
    {
        [SetUp]
        public new void Setup()
        {
            CreateTours();    
        }

        [Test]
        public void GetTours_should_return_result()
        {
            var res = Client.Get(new GetTours { Code = "1" });
            res.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetTours_should_throw_exception()
        {
            Client.Invoking(x => x.Get(new GetTours()))
                .ShouldThrow<WebServiceException>()
                .Which.StatusCode.Should().Be(404);
        }

        public void CreateTours()
        {
            Db.Insert(new Tour { Code = "1" });

        }
    }
}
