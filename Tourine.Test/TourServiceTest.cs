using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;
using Tourine.Test.Common;
using TourPassengers = Tourine.ServiceInterfaces.Tours.TourPassengers;

namespace Tourine.Test
{
    public class TourServiceTest : ServiceTest<TourService>
    {
        private readonly TourDetail _tourDetail = new TourDetail();
        private readonly Tour _tour = new Tour();
        private readonly Person _person = new Person();
        private readonly Team _team = new Team();
        private readonly TourOption _tourOption = new TourOption();
        private readonly List<TourOption> _optionList = new List<TourOption>();
        private PassengerList _passenger;
        private Agency _agency = new Agency();

        [SetUp]
        public new void Setup()
        {
            CreateTours();
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
            var res = (QueryResponse<TourOption>)MockService.Get(new GetTourOptions { TourId = _tour.Id });
            res.Results.Count.Should().Be(1);
        }

        [Test]
        public void GetTour_should_return_result()
        {
            var res = (Tour)MockService.Get(new GetTour { Id = _tour.Id });
            res.Id.Should().Be(_tour.Id);
        }

        [Test]
        public void GetTour_should_throw_exception()
        {
            new Action(() => MockService.Get(new GetTour { Id = Guid.NewGuid() }))
                .ShouldThrow<HttpError>();
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
        public void DeleteTour_when_block_is_true_should_not_remove_tourDetail()
        {
            var tourId = Guid.NewGuid();
            var blockId = Guid.NewGuid();
            var tourDetailId = Guid.NewGuid();
            Db.Insert(new TourDetail { Id = tourDetailId });
            Db.Insert(new Tour { Id = tourId, TourDetailId = tourDetailId });
            Db.Insert(new Tour { Id = blockId, ParentId = tourId, TourDetailId = tourDetailId });
            //
            MockService.Delete(new DeleteTour { Id = blockId });
            Db.SingleById<TourDetail>(tourDetailId).Should().NotBeNull();
        }

        [Test]
        public void DeleteTour_when_has_block_should_throw_exception()
        {
            var tourId = Guid.NewGuid();
            var blockId = Guid.NewGuid();
            var tourDetailId = Guid.NewGuid();
            Db.Insert(new TourDetail { Id = tourDetailId });
            Db.Insert(new Tour { Id = tourId, TourDetailId = tourDetailId });
            Db.Insert(new Tour { Id = blockId, ParentId = tourId, TourDetailId = tourDetailId });
            new Action(() => MockService.Delete(new DeleteTour { Id = tourId }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void DeleteTour_when_has_passenger_should_throw_exception()
        {
            new Action(() => MockService.Delete(new DeleteTour { Id = _tour.Id }))
                            .ShouldThrow<HttpError>();
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
        public void GetPersonOfTour_should_return_result()
        {
            var res = (TourPassengers)MockService.Get(new GetPersonsOfTour { TourId = _tour.Id });
            res.Leader.Id.Should().Be(_person.Id);
        }

        [Test]
        public void UpdateBlock_should_store_dependecies()
        {
            var id = Guid.NewGuid();
            Db.Insert(new Tour { Id = id, Capacity = 1 });
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
            Db.Insert(new TourDetail { Id = id });
            Db.Insert(new Tour { Id = id, Capacity = 1, TourDetailId = id });
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
            //is options store
            var savedOptions = Db.Select<TourOption>(x => x.TourId == newBlock.Id);
            savedOptions.Count.Should().Be(1);
            savedOptions[0].ShouldBeEquivalentTo(request.Options[0], x => x.Excluding(y => y.SelectedMemberPath.Matches("*Id")));
            //is detail store
            var tourDetail = Db.Select<Tour>(x => x.Id == newBlock.Id && x.TourDetailId == id);
            tourDetail.Count.Should().Be(1);
        }

        [Test]
        public void PassengerReplacementTourAccomplish_should_not_throw_exception()
        {
            new Action(() => MockService.Put(new PassengerReplacementTourAccomplish { TourId = _tour.Id, BasePrice = 1, InfantPrice = 1, BusPrice = 1, FoodPrice = 1, RoomPrice = 1 }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void IsDeleteable_should_throw_exception()
        {
            try
            {
                _tour.IsDeleteable(Db);
                Assert.Fail();
            }
            catch (HttpError)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void GetTourFreeSpace_should_throw_exception()
        {
            new Action(() => MockService.Get(new GetTourFreeSpace { TourId = Guid.NewGuid() }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void GetTourFreeSpace_should_return_result()
        {
            var res = MockService.Get(new GetTourFreeSpace { TourId = _tour.Id });
            res.Should().Be("0");
        }

        [Test]
        public void GetBlocks_should_return_result()
        {
            var res = (QueryResponse<Tour>)MockService.Get(new GetBlocks { TourId = _tour.Id });
            res.Results[0].Id.Should().Be(_tour.Id);
        }

        [Test]
        public void FetTourAgency_should_return_result()
        {
            var list = (IList<Tour>)MockService.Get(new GetTourAgency {TourId = _tour.Id, LoadChild = false});
            list[0].AgencyId = _agency.Id;
        }

        public void CreateTours()
        {
            
            _tour.AgencyId = _agency.Id;
            _tour.TourDetailId = _tourDetail.Id;
            _tourOption.TourId = _tour.Id;
            _optionList.Add(_tourOption);
            _optionList.Add(_tourOption);
            _optionList.Add(_tourOption);
            _tourDetail.LeaderId = _person.Id;
            _passenger = new PassengerList
            {
                PersonId = _person.Id,
                TourId = _tour.Id,
                TeamId = _team.Id,
            };

            Db.Insert(_tourDetail);
            Db.Insert(_tour);
            Db.Insert(_person);
            Db.Insert(_tourOption);
            Db.Insert(_passenger);
            InsertDb(_agency, true);
        }
    }
}
