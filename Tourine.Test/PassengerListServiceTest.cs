using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Tourine.Test
{
    public class PassengerListServiceTest : ServiceTest<PassengerListService>
    {
        private readonly Tour _tour = new Tour();
        private readonly TourDetail _tourDetail = new TourDetail();
        private readonly Team _team = new Team();
        private readonly Person _person = new Person();

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

        private void CreateSampleTour()
        {
            _tour.TourDetailId = _tourDetail.Id;
            _team.TourId = _tour.Id;
            _tourDetail.LeaderId = _person.Id;

            Db.Insert(_person);
            Db.Insert(_tour);
            Db.Insert(_tourDetail);
            Db.Insert(_team);
            Db.Insert(new PassengerList { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Bus, HaveVisa = true });
            Db.Insert(new PassengerList { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Room, HaveVisa = true });
            Db.Insert(new PassengerList { PersonId = _person.Id, TourId = _tour.Id, TeamId = _person.Id, OptionType = OptionType.Food, HaveVisa = true });
        }
    }
}
