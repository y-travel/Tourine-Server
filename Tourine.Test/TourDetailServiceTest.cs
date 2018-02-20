using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class TourDetailServiceTest : ServiceTest<TourDetailService>
    {
        private readonly Guid _testTourDetailGuid = Guid.NewGuid();
        private readonly Guid _testDestinationGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
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
            var item = Client.Get(new GetTourDetail {Id = _testTourDetailGuid});
            item.DestinationId.Should().Be(_testDestinationGuid);
        }

        [Test]
        public void UpdateTourDetail_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new UpdateTourDetail
            {
                TourDetail = new TourDetail
                {
                    Id = Guid.NewGuid(),
                    DestinationId = _testDestinationGuid,
                    CreationDate = DateTime.Now
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void UpdateTourDetail_should_not_throw_exception()
        {
            Client.Invoking(x => x.Put(new UpdateTourDetail
            {
                TourDetail = new TourDetail
                {
                    Id = _testTourDetailGuid,
                    DestinationId = _testDestinationGuid,
                    CreationDate = DateTime.Now
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        public void CreateTourDetail()
        {
            Db.Insert(new TourDetail
            {
                Id = _testTourDetailGuid,
                DestinationId = _testDestinationGuid,
                CreationDate = DateTime.Today,
                PlaceId = Guid.NewGuid(),
            });

            Db.Insert(new Destination {Id = _testDestinationGuid, Name = "destination"});
        }

    }
}
