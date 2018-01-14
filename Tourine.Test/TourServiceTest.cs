using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models;
using Tourine.Models.DatabaseModels;
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
            var tourInfo = Client.Get<Tour>(new GetTour { Id = _testTourId });
            tourInfo.Should().NotBeNull();
            tourInfo.Destination.Should().NotBeNull();
            tourInfo.Place.Should().NotBeNull();
        }

        public void CreateTours()
        {
            var testPdId = Guid.NewGuid();
            var testDId = Guid.NewGuid();
            var testPId = Guid.NewGuid();
            Db.Insert(new Place { Id = testPId, Name = "Hotel" });
            Db.Insert(new Destination { Id = testDId, Name = "Karbala" });
            Db.Insert(new Currency { Id = 1, Name = "Rial", Factor = 1 });
            Db.Insert(new PriceDetail { Id = testPdId, CurrencyId = 1, Value = 4000 });
            Db.Insert(new Tour { Id = _testTourId, DestinationId = testDId, PlaceId = testPId, Status = TourStatus.Created });

        }

        [Test]
        public void PostTour_should_not_return_exception()
        {
            Client.Invoking(x => x.Post(new PostTour
            {
                Tour = new Tour
                {
                    Id = Guid.NewGuid(),
                    Code = "555",
                    Capacity = 5,
                    DestinationId = Guid.NewGuid(),
                    PlaceId = Guid.NewGuid(),
                    Duration = 12,
                    IsFlight = true,
                    AdultCount = 80,
                    AdultMinPrice = 8000,
                    InfantPrice = 65000,
                    BusPrice = 50000,
                    RoomPrice = 45000,
                    FoodPrice = 35000
                }
            })).ShouldNotThrow<WebServiceException>();
        }
    }
}
