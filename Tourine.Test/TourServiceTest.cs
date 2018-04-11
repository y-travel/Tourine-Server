using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Persons;
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
        private readonly Person _leader = new Person { Name = "any" };
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
        public void DeleteTour_when_block_is_true_should_remove_just_block()
        {
            var tourId = Guid.NewGuid();
            var blockId = Guid.NewGuid();
            Db.Insert(new Tour { Id = tourId });
            Db.Insert(new Tour { Id = blockId, ParentId = tourId });
            //
            MockService.Delete(new DeleteTour { Id = blockId });
            Db.SingleById<Tour>(tourId).Should().NotBeNull();
        }

        [Test]
        public void DeleteTour_when_block_is_false_should_remove_tourDetail()
        {
            var tourId = Guid.NewGuid();
            var blockId = Guid.NewGuid();
            var tourDetailId = Guid.NewGuid();
            Db.Insert(new TourDetail { Id = tourDetailId });
            Db.Insert(new Tour { Id = tourId });
            Db.Insert(new Tour { Id = blockId, ParentId = tourId });
            //
            MockService.Delete(new DeleteTour { Id = tourId });
            Db.SingleById<TourDetail>(tourDetailId).Should().NotBeNull();
        }
        [Test]
        public void UpdateTour_should_save_tour()
        {
            var id = Guid.NewGuid();
            Db.Insert(new Tour { Id = id });
            Db.Insert(new TourDetail { Id = id });
            Db.Insert(new TourOption { Id = id });
            var request = new UpsertTour
            {
                Id = id,
                Capacity = 1,
                BasePrice = 3000,
                TourDetail = new TourDetail
                {
                    Id = id,
                    DestinationId = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    PlaceId = Guid.NewGuid()
                },
                Options = new[]
                {
                    new TourOption
                    {
                        Id = id,
                        OptionType = OptionType.Bus,
                        Price = 1,
                    }
                }.ToList(),
            };
            //act
            MockService.Post(request);
            //assert
            var newTourDetail = Db.SingleById<TourDetail>(id);
            var newOptions = Db.Select<TourOption>(x => x.TourId == id);
            newTourDetail.ShouldBeEquivalentTo(request.TourDetail);
            newOptions[0].ShouldBeEquivalentTo(request.Options[0]);

        }

        [Test]
        public void CreateTour_should_save_tour()
        {
            var id = Guid.Empty;
            var request = new UpsertTour
            {
                Id = id,
                Capacity = 1,
                BasePrice = 3000,
                TourDetail = new TourDetail
                {
                    Id = id,
                    DestinationId = Guid.NewGuid(),
                    StartDate = DateTime.Now,
                    PlaceId = Guid.NewGuid()
                },
                Options = new[]
                {
                    new TourOption
                    {
                        OptionType = OptionType.Bus,
                        OptionStatus = OptionStatus.Limited,
                        Price = 1,
                    }
                }.ToList(),
            };
            //act
            var returnedTour = (Tour)MockService.Post(request);
            //assert
            var savedTourDetail = Db.SingleById<TourDetail>(returnedTour.TourDetailId);
            savedTourDetail.ShouldBeEquivalentTo(request.TourDetail);
            var savedOptions = Db.Select<TourOption>(x => x.TourId == returnedTour.Id);
            savedOptions.Count.Should().Be(1);
            savedOptions[0].ShouldBeEquivalentTo(request.Options[0], x => x.Excluding(y => y.SelectedMemberPath.Matches("*Id")));
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

        [Test]
        public void GetPersonOfTour_should_return_result()
        {
            var res = (TourPassengers)MockService.Get(new GetPersonsOfTour { TourId = _testTourId });
            res.Leader.Id.Should().Be(_leader.Id);
        }

        [Test]
        public void UpdateBlock_should_store_dependecies()
        {
            var id = Guid.NewGuid();
            Db.Insert(new Tour { Id = id, Capacity = 1, ParentId = id });
            Db.Insert(new Agency { Id = id });
            Db.Insert(new TourOption { Id = id });
            var request = new UpsertTour
            {
                Id = id,
                AgencyId = id,
                ParentId = id,
                InfantPrice = 1,
                BasePrice = 1,
                Capacity = 1,
                Options = new List<TourOption>
                {
                    new TourOption
                    {
                        Id=id,
                        OptionType = OptionType.Bus,
                        OptionStatus = OptionStatus.Limited,
                        Price = 1,
                    }
                }
            };
            var newBlock = (Tour)MockService.Post(request);
            var savedOptions = Db.Select<TourOption>(x => x.TourId == newBlock.Id);
            savedOptions.Count.Should().Be(1);
            savedOptions[0].ShouldBeEquivalentTo(request.Options[0], x => x.Excluding(y => y.SelectedMemberPath.Matches("*Id")));
        }

        [Test]
        public void CreateBlock_should_store_dependecies()
        {
            var id = Guid.NewGuid();
            Db.Insert(new Tour { Id = id, Capacity = 1 });
            Db.Insert(new Agency { Id = id });
            Db.Insert(new TourOption { Id = id });
            var request = new UpsertTour
            {
                AgencyId = id,
                ParentId = id,
                InfantPrice = 1,
                BasePrice = 1,
                Capacity = 1,
                Options = new List<TourOption>
                {
                    new TourOption
                    {
                        
                        OptionType = OptionType.Bus,
                        OptionStatus = OptionStatus.Limited,
                        Price = 1,
                    }
                }
            };
            var newBlock = (Tour)MockService.Post(request);
            var savedOptions = Db.Select<TourOption>(x => x.TourId == newBlock.Id);
            savedOptions.Count.Should().Be(1);
            savedOptions[0].ShouldBeEquivalentTo(request.Options[0], x => x.Excluding(y => y.SelectedMemberPath.Matches("*Id")));
        }

        public void CreateTours()
        {
            var testDId = Guid.NewGuid();
            var testPId = Guid.NewGuid();
            Db.Insert(new Place { Id = testPId, Name = "Bed" });
            Db.Insert(new Destination { Id = testDId, Name = "Karbala" });
            Db.Insert(_leader);
            Db.Insert(new TourDetail
            {
                Id = _testTourDetaiGuid,
                DestinationId = testDId,
                PlaceId = testPId,
                LeaderId = _leader.Id,
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
