using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Places;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class TourServiceTest : ServiceTest<TourService>
    {
        private readonly Guid _testTourId = Guid.NewGuid();
        private readonly Guid _testTourDetaiGuid = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreateTours();
            //            AppHost.Session = new AuthSession
            //            {
            //                TestMode = true,
            //                User = new User { Id = Guid.NewGuid(), Role = Role.Admin },
            //                Agency = new Agency { Id = Guid.NewGuid() },
            //                Roles = Role.Admin.ParseRole<string>()
            //            };
        }

        [Test]
        public void GetTours_should_return_result()
        {
            var res = (QueryResponse<Tour>)MockService.Get(new GetTours());
            res.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetTourOptions_should_return_result()
        {
            Db.Insert(new TourOption { TourId = _testTourId, Price = 1000, });
            Db.Insert(new TourOption { TourId = _testTourId, Price = 2000, });
            Db.Insert(new TourOption { TourId = new Guid(), Price = 2000, });

            var res = (QueryResponse<TourOption>)MockService.Get(new GetTourOptions { TourId = _testTourId });
            res.Results.Count.Should().Be(2);
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
            Client.Invoking(x => x.Post(new UpsertTour
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
        [TestCase(true)]
        [TestCase(false)]
        public void UpdateTour_should_save_tour(bool isUpdate)
        {
            var id = isUpdate ? Guid.NewGuid(): Guid.Empty;
            Db.Insert(new Tour { Id = id });
            Db.Insert(new TourDetail { Id = id });
            var tourReq = new UpsertTour
            {
                Id = id,
                Capacity = 1,
                BasePrice = 3000,
                TourDetail = new TourDetail
                {
                    Id=id,
                    DestinationId = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    PlaceId = Guid.NewGuid()
                },
                Options = new[]
                {
                    new TourOption
                    {
                        OptionType = OptionType.Bus,
                        Price = 1,
                    }
                }.ToList(),
            };
            var newTour = MockService.Post(tourReq);

            newTour.Should().NotBeNull();
        }
        [Test]
        public void CreateTour_should_return_exception()
        {
            Client.Invoking(x => x.Post(new UpsertTour
            {
                BasePrice = 300000,
                TourDetail = new TourDetail
                {
                    DestinationId = Guid.NewGuid()
                }
            })).ShouldThrow<WebServiceException>();
        }

        //        [Test]
        //        public void UpdateTour_should_not_throw_exceprion()
        //        {
        //            Client.Invoking(x => x.Put(new UpsertTour
        //            {
        //                Tour = new Tour
        //                {
        //                    Id = _testTourId,
        //                    Code = "aio",
        //                    Status = TourStatus.Created,
        //                    Capacity = 500,
        //                    BasePrice = 300000,
        //                    TourDetailId = Guid.NewGuid(),
        //                    AgencyId = Guid.NewGuid()
        //                }
        //            })).ShouldNotThrow<WebServiceException>();
        //        }

        //        [Test]
        //        public void UpdateTour_should_throw_exceprion()
        //        {
        //            Client.Invoking(x => x.Put(new UpsertTour
        //            {
        //                Tour = new Tour
        //                {
        //                    Id = Guid.NewGuid(),
        //                    Code = "123456",
        //                    Status = TourStatus.Created,
        //                    Capacity = 50,
        //                    BasePrice = 1200000,
        //                    TourDetailId = Guid.NewGuid(),
        //                    AgencyId = Guid.NewGuid()
        //                }
        //            })).ShouldThrow<WebServiceException>();
        //        }
        public void CreateTours()
        {
            var testDId = Guid.NewGuid();
            var testPId = Guid.NewGuid();
            Db.Insert(new Place { Id = testPId, Name = "Bed" });
            Db.Insert(new Destination { Id = testDId, Name = "Karbala" });
            Db.Insert(new TourDetail
            {
                Id = _testTourDetaiGuid,
                DestinationId = testDId,
                PlaceId = testPId,
            });

            Db.Insert(new Tour
            {
                Id = _testTourId,
                Code = "123456",
                Status = TourStatus.Created,
                Capacity = 50,
                BasePrice = 1200000,
                TourDetailId = _testTourDetaiGuid,
                AgencyId = CurrentAgency.Id,

            });
        }
    }
}
