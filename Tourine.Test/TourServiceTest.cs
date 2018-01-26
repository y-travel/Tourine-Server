using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Places;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.Test
{
    public class TourServiceTest : ServiceTest
    {
        private readonly Guid _testTourId = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreateTours();
        }

        [Test]
        public void GetTours_should_return_result()
        {
            var res = Client.Get(new GetTours());
            res.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetTour_should_return_result()
        {
            var dto = new GetTour();
            dto.Id = _testTourId;
            var tourInfo = Client.Get<Tour>(dto);
            tourInfo.Id.Should().Be(_testTourId);
        }

        [Test]
        public void GetTour_should_throw_exception()
        {
            Client.Invoking(t => t.Get(new GetTour
                {
                    Id = Guid.NewGuid()
                }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PostTour_should_not_return_exception()
        {
            Client.Invoking(x => x.Post(new PostTour
            {
                Tour = new Tour
                {
                    Code = "555",
                    DestinationId = Guid.NewGuid(),
                    PlaceId = Guid.NewGuid(),
                    Duration = 12,
                    IsFlight = true,
                    AdultCount = 80,
                    InfantCount = 10,
                    AdultMinPrice = 8000,
                    InfantPrice = 65000,
                    BusPrice = 50000,
                    RoomPrice = 45000,
                    FoodPrice = 35000,
                    StartDate = DateTime.MaxValue
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void PostTour_should_return_exception()
        {
            Client.Invoking(x => x.Post(new PostTour
            {
                Tour = new Tour
                {
                    Code = "",
                    DestinationId = Guid.NewGuid(),
                    PlaceId = Guid.NewGuid(),
                    Duration = 12,
                    IsFlight = true,
                    AdultCount = 80,
                    InfantCount = 10,
                    AdultMinPrice = 8000,
                    InfantPrice = 65000,
                    BusPrice = 50000,
                    RoomPrice = 45000,
                    FoodPrice = 35000,
                    StartDate = DateTime.MinValue
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutTour_should_not_throw_exceprion()
        {
            Client.Invoking(x=> x.Put(new PutTour{ Tour = new Tour
            {
                Id = _testTourId,
                AdultCount = 12,
                AdultMinPrice = 1200,
                InfantCount = 12,
                InfantPrice = 3000,
                BusPrice = 120,
                Code = "123",
                FoodPrice = 100,
                DestinationId = Guid.NewGuid(),
                IsFlight = true,
                Duration = 1,
                PlaceId = Guid.NewGuid(),
                RoomPrice = 16,
                StartDate = DateTime.Now
            }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void PutTour_should_throw_exceprion()
        {
            Client.Invoking(x => x.Put(new PutTour
            {
                Tour = new Tour
                {
                    Id = Guid.NewGuid(),
                    AdultCount = 12,
                    AdultMinPrice = 1200,
                    InfantCount = 12,
                    InfantPrice = 3000,
                    BusPrice = 120,
                    Code = "123",
                    FoodPrice = 100,
                    DestinationId = Guid.NewGuid(),
                    IsFlight = true,
                    Duration = 1,
                    PlaceId = Guid.NewGuid(),
                    RoomPrice = 16,
                    StartDate = DateTime.Now
                }
            })).ShouldThrow<WebServiceException>();
        }
        public void CreateTours()
        {
            var testDId = Guid.NewGuid();
            var testPId = Guid.NewGuid();
            Db.Insert(new Place { Id = testPId, Name = "Hotel" });
            Db.Insert(new Destination { Id = testDId, Name = "Karbala" });
            Db.Insert(new Tour
            {
                Id = _testTourId,
                DestinationId = testDId,
                AdultCount = 12,
                InfantCount = 10,
                Code = "123456",
                AdultMinPrice = 1200,
                BusPrice = 120,
                Duration = 10,
                FoodPrice = 120,
                RoomPrice = 130,
                PlaceId = testPId,
                Status = TourStatus.Created,
                StartDate = DateTime.Now,
                InfantPrice = 100,
                IsFlight = true
            });

        }
    }
}
