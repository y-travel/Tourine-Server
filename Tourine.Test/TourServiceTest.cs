using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using Tourine.Models;
using Tourine.Models.DatabaseModels;
using Tourine.Models.ServiceModels;

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
            tourInfo.PriceDetail.Should().NotBeNull();
            tourInfo.Status.Should().NotBeNull();
        }

        public void CreateTours()
        {
            var testPdId = Guid.NewGuid();
            var testDId = Guid.NewGuid();
            var testPId = Guid.NewGuid();
            Db.Insert(new Place { Id = testPId, Name = "Hotel" });
            Db.Insert(new Destination { Id = testDId, Name = "Karbala" });
            Db.Insert(new Currency { Id = 1, Name = "Rial", Factor = "1" });
            Db.Insert(new PriceDetail { Id = testPdId, CurrencyId = 1, Price = 4000 });
            Db.Insert(new Tour { Id = _testTourId, PriceDetailId = testPdId, DestinationId = testDId, PlaceId = testPId, StatusId = 1 });
            Db.Insert(new Status { Id = 1, Name = "expired" });
        }

        [Test]
        public void PostTour_must_return_new_result()
        {
            var res = Client.Post(new PostTour
            {
                Tour = new Tour
                {
                    Id = Guid.NewGuid(),
                    Code = "555",
                    Capacity = 5,
                    PriceDetailId = Guid.NewGuid(),
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
            });

            res.Code.Should().Be("555");
        }
    }
}
