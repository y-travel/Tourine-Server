using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class TourDetailServiceTest : ServiceTest<TourDetailService>
    {
        private readonly Guid _testTourDetailGuid = Guid.NewGuid();
        private readonly Guid _testDestinationGuid = Guid.NewGuid();

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            CreateTourDetail();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User {Id = Guid.NewGuid()}
            };
        }

        [Test]
        public void GetTourDetail_should_return_result()
        {
            var item = (TourDetail)MockService.Get(new GetTourDetail {Id = _testTourDetailGuid});

            item.DestinationId.Should().Be(_testDestinationGuid);
        }

        public void CreateTourDetail()
        {
            Db.Insert(new TourDetail
            {
                Id = _testTourDetailGuid,
                DestinationId = _testDestinationGuid,
                PlaceId = Guid.NewGuid(),
            });

            Db.Insert(new Destination {Id = _testDestinationGuid, Name = "destination"});
        }

    }
}
