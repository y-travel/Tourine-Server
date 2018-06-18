using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class TeamServiceTest : ServiceTest<TeamService>
    {
        private readonly Tour _tour = new Tour { Capacity = 2, FreeSpace = 2 };
        private readonly Person _person1 = new Person();
        private readonly Person _person2 = new Person();
        private readonly Person _person3 = new Person();

        private readonly List<PassengerInfo> _passengers = new List<PassengerInfo>();

        [SetUp]
        public new void Setup()
        {

            var teamMember1 = new PassengerInfo { PersonId = _person1.Id, OptionType = OptionType.Bus, Person = _person1 };
            var teamMember2 = new PassengerInfo { PersonId = _person2.Id, OptionType = OptionType.Food, Person = _person2 };
            var teamMember3 = new PassengerInfo { PersonId = _person3.Id, OptionType = OptionType.Room, Person = _person3 };

            _passengers.Add(teamMember1);
            _passengers.Add(teamMember2);
            _passengers.Add(teamMember3);

            CreateTeam();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = Guid.NewGuid() }
            };
        }

        [Test]
        public void UpsertTeam_shoud_not_throw_exception()
        {
            new Action(() => MockService.Post(new UpsertTeam
            {
                TourId = _tour.Id,
                Buyer = _passengers[0].Person,
                Passengers = _passengers.GetRange(1, 1)
            }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void UpsertTeam_should_throw_exception()//why: not free enugh space in this tour (have 2 free space but need 3)
        {
            new Action(() => MockService.Post(new UpsertTeam
            {
                TourId = _tour.Id,
                Buyer = _passengers[0].Person,
                Passengers = _passengers
            }))
                .ShouldThrow<HttpError>();
        }

        [Test]
        public void UpsertTeam_should_not_throw_exception()
        {
            new Action(() => MockService.Post(new UpsertTeam
            {
                TourId = _tour.Id,
                Buyer = _passengers[0].Person,
                Passengers = _passengers.GetRange(1, 2)
            }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void GetTourTeams_shoudl_return_result()
        {
            Db.Insert(new Team { Buyer = _person1, BuyerId = _person1.Id, Tour = _tour, TourId = _tour.Id });

            var res = (QueryResponse<Team>)MockService.Get(new GetTourTeams { TourId = _tour.Id });
            res.Results.Count.Should().Be(1);
        }

        [Test]
        public void DeleteTeam_shoudl_not_throw_exception()
        {
            var team = new Team { Buyer = _person1, BuyerId = _person1.Id, Tour = _tour, TourId = _tour.Id };
            Db.Insert(team);
            new Action(() => MockService.Delete(new DeleteTeam { TeamId = team.Id }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void GetPersonOfTeam_shoudl_return_result()
        {
            var tm = (Team)MockService.Post(new UpsertTeam
            {
                TourId = _tour.Id,
                Buyer = _passengers[0].Person,
                Passengers = _passengers.GetRange(1, 1)
            });

            var res = (TeamPassengers)MockService.Get(new GetPersonsOfTeam { TeamId = tm.Id });
            res.Passengers.Should().NotBeNull();
        }

        public void CreateTeam()
        {
            Db.Insert(_person1);
            Db.Insert(_person2);
            Db.Insert(_person3);
            Db.Insert(_tour);
        }
    }
}
