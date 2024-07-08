using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class PassengerServiceTest : ServiceTest<PassengerService>
    {
        private readonly Tour _tour = new Tour { Capacity = 20 };
        private readonly TourDetail _tourDetail = new TourDetail();
        private readonly Team _team = new Team();
        private readonly Person _person = new Person { Name = "name", Family = "family", MobileNumber = "mobileNumber" };

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            CreateSampleTour();
        }

        [Test]
        public void GetTourPassengers_should_return_passenger_depend_on_visa()
        {
            var res = (TourPassengers)MockService.Get(new GetTourPassengers { TourId = _tour.Id });
            res.Leader.Id.Should().Be(_person.Id);
            res.Passengers.Count.Should().Be(3);
        }

        [Test]
        public void GetTourBuyers_should_return_results()
        {
            long[,] numericValue = { { 100, 2 }, { 480, 10 }, { 240, 5 }, { 100, 2 }, { 150, 3 }, { 60, 1 }, { 300, 6 }, { 55, 1 }, { 52, 1 } };

            Db.Insert(new Team { TourId = _tour.Id, BuyerId = _person.Id, TotalPrice = numericValue[0, 0], Count = (int)numericValue[0, 1] });
            Db.Insert(new Team { TourId = _tour.Id, BuyerId = _person.Id, TotalPrice = numericValue[1, 0], Count = (int)numericValue[1, 1] });
            Db.Insert(new Team { TourId = _tour.Id, BuyerId = _person.Id, TotalPrice = numericValue[2, 0], Count = (int)numericValue[2, 1] });
            var agency1 = new Agency();
            var agency2 = new Agency();
            Db.Insert(agency1);
            Db.Insert(agency2);
            var tour1 = new Tour { Capacity = 5, ParentId = _tour.Id, AgencyId = agency1.Id };//same agency merged to one row
            var tour2 = new Tour { Capacity = 7, ParentId = _tour.Id, AgencyId = agency1.Id };//same agency
            var tour3 = new Tour { Capacity = 2, ParentId = _tour.Id, AgencyId = agency2.Id };
            Db.Insert(tour1);
            Db.Insert(tour2);
            Db.Insert(tour3);
            Db.Insert(new Team { TourId = tour1.Id, TotalPrice = numericValue[3, 0], Count = (int)numericValue[3, 1] });
            Db.Insert(new Team { TourId = tour1.Id, TotalPrice = numericValue[4, 0], Count = (int)numericValue[4, 1] });

            long totalPrice = 0;
            long totalCount = 0;
            for (var i = 0; i < numericValue.Length / 2; i++)
            {
                totalPrice += numericValue[i, 0];
                totalCount += numericValue[i, 1];
            }

            var results = (IList<TourBuyer>)MockService.Get(new GetTourBuyers { TourId = _tour.Id });
            results.Count.Should().Be(5);
            results.Sum(x => x.Price).Should().Be(totalPrice);
            results.Sum(x => x.Count).Should().Be((int)totalCount);
        }

        private void CreateSampleTour()
        {
            _tour.TourDetailId = _tourDetail.Id;
            _team.TourId = _tour.Id;
            _tourDetail.LeaderId = _person.Id;

            Db.Insert(_person);
            Db.Insert(_tour);
            Db.Insert(_tourDetail);
            Db.Insert(_team);
            Db.Insert(new Passenger { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Bus, HasVisa = true });
            Db.Insert(new Passenger { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Room, HasVisa = true });
            Db.Insert(new Passenger { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Food, HasVisa = true });
        }
    }
}
