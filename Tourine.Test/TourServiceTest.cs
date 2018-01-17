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
            try
            {
   var tourInfo = Client.Get<Tour>(dto);
                tourInfo.Id.Should().Be(_testTourId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
         
         
/*            tourInfo.Should().NotBeNull();
            tourInfo.Destination.Should().NotBeNull();
            tourInfo.Place.Should().NotBeNull();*/
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
                    FoodPrice = 35000
                }
            })).ShouldNotThrow<WebServiceException>();
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
