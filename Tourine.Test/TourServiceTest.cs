using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Places;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class TourServiceTest : ServiceTest
    {
        private readonly Guid _testTourId = Guid.NewGuid();
        private readonly Guid _testTourDetaiGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateTours();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = Guid.NewGuid(), Role = Role.Admin },
                Agency = new Agency { Id = Guid.NewGuid() },
                Roles = Role.Admin.ParseRole<string>()
            };
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
        public void CreateTour_should_not_return_exception()
        {
            Client.Invoking(x => x.Post(new CreateTour
            {
                Capacity = 500,
                BasePrice = 300000
                ,
                TourDetail = new TourDetail
                {
                    DestinationId = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    PlaceId = Guid.NewGuid(),
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void CreateTour_should_save_details()
        {
            var tour = Client.Post(new CreateTour
            {
                Capacity = 1,
                BasePrice = 3000,
                TourDetail = new TourDetail
                {
                    DestinationId = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    PlaceId = Guid.NewGuid()
                }
            });
            tour.TourDetail.Should().NotBeNull();
        }
        [Test]
        public void CreateTour_should_return_exception()
        {
            Client.Invoking(x => x.Post(new CreateTour
            {
                BasePrice = 300000,
                TourDetail = new TourDetail
                {
                    DestinationId = Guid.NewGuid()
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void UpdateTour_should_not_throw_exceprion()
        {
            Client.Invoking(x => x.Put(new UpdateTour
            {
                Tour = new Tour
                {
                    Id = _testTourId,
                    Code = "aio",
                    Status = TourStatus.Created,
                    Capacity = 500,
                    BasePrice = 300000,
                    TourDetailId = Guid.NewGuid(),
                    AgencyId = Guid.NewGuid()
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void UpdateTour_should_throw_exceprion()
        {
            Client.Invoking(x => x.Put(new UpdateTour
            {
                Tour = new Tour
                {
                    Id = Guid.NewGuid(),
                    Code = "123456",
                    Status = TourStatus.Created,
                    Capacity = 50,
                    BasePrice = 1200000,
                    TourDetailId = Guid.NewGuid(),
                    AgencyId = Guid.NewGuid()
                }
            })).ShouldThrow<WebServiceException>();
        }
        public void CreateTours()
        {
            var testDId = Guid.NewGuid();
            var testPId = Guid.NewGuid();
            Db.Insert(new Place { Id = testPId, Name = "Hotel" });
            Db.Insert(new Destination { Id = testDId, Name = "Karbala" });
            Db.Insert(new TourDetail
            {
                Id = _testTourDetaiGuid,
                DestinationId = testDId,
                PlaceId = testPId,
                CreationDate = DateTime.Today
            });

            Db.Insert(new Tour
            {
                Id = _testTourId,
                Code = "123456",
                Status = TourStatus.Created,
                Capacity = 50,
                BasePrice = 1200000,
                TourDetailId = _testTourDetaiGuid,
                AgencyId = Guid.NewGuid()

            });

        }
    }
}
