using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Html;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class PassengerListServiceTest : ServiceTest<PassengerListService>
    {
        private readonly Tour _tour = new Tour { Capacity = 20 };
        private readonly TourDetail _tourDetail = new TourDetail();
        private readonly Team _team = new Team();
        private readonly Person _person = new Person { Name = "name", Family = "family", MobileNumber = "mobileNumber" };

        [SetUp]
        public new void setup()
        {
            CreateSampleTour();
        }

        [Test]
        public void duplication_in_passenger_list_upsert_should_throw_exception()
        {

        }

        [Test]
        public void GetTourVisa_should_return_result()
        {
            var result = (TourPersonReport)MockService.Get(new GetTourVisa { TourId = _tour.Id, Have = true });
            result.Leader.Id.Should().Be(_person.Id);
            result.Tour.Id.Should().Be(_tour.Id);
            result.Passengers[0].Id.Should().Be(_person.Id);
        }

        [Test]
        public void GetTourVisa_should_return_zero_passenger()
        {
            var result = (TourPersonReport)MockService.Get(new GetTourVisa { TourId = _tour.Id, Have = false });
            result.Leader.Id.Should().Be(_person.Id);
            result.Tour.Id.Should().Be(_tour.Id);
            result.Passengers.Count.Should().Be(0);
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

            Db.Insert(new Team { TourId = tour2.Id, TotalPrice = numericValue[5, 0], Count = (int)numericValue[5, 1] });
            Db.Insert(new Team { TourId = tour2.Id, TotalPrice = numericValue[6, 0], Count = (int)numericValue[6, 1] });

            Db.Insert(new Team { TourId = tour3.Id, TotalPrice = numericValue[7, 0], Count = (int)numericValue[7, 1] });
            Db.Insert(new Team { TourId = tour3.Id, TotalPrice = numericValue[8, 0], Count = (int)numericValue[8, 1] });

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
            Db.Insert(new PassengerList { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Bus, HasVisa = true });
            Db.Insert(new PassengerList { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Room, HasVisa = true });
            Db.Insert(new PassengerList { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Food, HasVisa = true });
        }
    }
}
