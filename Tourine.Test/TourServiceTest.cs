using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using Tourine.Models;

namespace Tourine.Test
{
    public class TourServiceTest : ServiceTest
    {
        private Guid _testTourId = Guid.NewGuid();
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
            /*            tourInfo.Place.Should().BeNull();
                        tourInfo.PriceDetail.Should().BeNull();*/
        }

        /*        [Test]
                public void GetTours_should_throw_exception()
                {
                    Client.Invoking(x => x.Get(new GetTours()))
                        .ShouldThrow<WebServiceException>()
                        .Which.StatusCode.Should().Be(404);
                }*/

        public void CreateTours()
        {
            var testPDId = Guid.NewGuid();
            var testDId = Guid.NewGuid();
            var testPId = Guid.NewGuid();
            Db.Insert(new Place { Id = testPDId, Name = "Hotel" });
            Db.Insert(new Destination { Id = testDId, Name = "Karbala" });
            Db.Insert(new Currency { Id = 1, Name = "Rial", Factor = "1" });
            Db.Insert(new PriceDetail { Id = testPId, CurrencyId = 1, Price = 4000 });
            Db.Insert(new Tour { Id = _testTourId, PriceDetailId = testPDId, DestinationId = testDId, PlaceId = testPId });
        }
    }
}
