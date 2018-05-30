﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;
using Tourine.ServiceInterfaces.Users;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class TeamServiceTest : ServiceTest<TeamService>
    {
        private readonly Tour tour = new Tour { Capacity = 2 , FreeSpace = 2};
        private readonly Person person_1 = new Person();
        private readonly Person person_2 = new Person();
        private readonly Person person_3 = new Person();

        private readonly List<TeamMember> _passengers = new List<TeamMember>();
        List<PersonIncome> personIncomes = new List<PersonIncome>();
        private readonly PersonIncome personBusIncome = new PersonIncome
        {
            OptionType = OptionType.Bus,
            IncomeStatus = IncomeStatus.Settled,
            ReceivedMoney = 12000
        };
        private readonly PersonIncome personFoodIncome = new PersonIncome
        {
            OptionType = OptionType.Food,
            IncomeStatus = IncomeStatus.Settled,
            ReceivedMoney = 15000
        };
        private readonly PersonIncome personRoomIncome = new PersonIncome
        {
            OptionType = OptionType.Room,
            IncomeStatus = IncomeStatus.Settled,
            ReceivedMoney = 18000
        };


        [SetUp]
        public new void Setup()
        {
            personIncomes.Add(personBusIncome);
            personIncomes.Add(personFoodIncome);
            personIncomes.Add(personRoomIncome);

            var teamMember_1 = new TeamMember { PersonId = person_1.Id, PersonIncomes = personIncomes, Person = person_1 };
            var teamMember_2 = new TeamMember { PersonId = person_2.Id, PersonIncomes = personIncomes, Person = person_2 };
            var teamMember_3 = new TeamMember { PersonId = person_3.Id, PersonIncomes = personIncomes, Person = person_3 };

            _passengers.Add(teamMember_1);
            _passengers.Add(teamMember_2);
            _passengers.Add(teamMember_3);

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
                TourId = tour.Id,
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
                TourId = tour.Id,
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
                    TourId = tour.Id,
                    Buyer = _passengers[0].Person,
                    Passengers = _passengers.GetRange(1, 2)
                }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void GetTourTeams_shoudl_return_result()
        {
            Db.Insert(new Team { Buyer = person_1, BuyerId = person_1.Id, Tour = tour, TourId = tour.Id });

            var res = (QueryResponse<Team>)MockService.Get(new GetTourTeams { TourId = tour.Id });
            res.Results.Count.Should().Be(1);
        }

        [Test]
        public void DeleteTeam_shoudl_not_throw_exception()
        {
            var team = new Team { Buyer = person_1, BuyerId = person_1.Id, Tour = tour, TourId = tour.Id };
            Db.Insert(team);
            new Action(() => MockService.Delete(new DeleteTeam { TeamId = team.Id }))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void GetPersonOfTeam_shoudl_return_result()
        {
            var tm = (Team)MockService.Post(new UpsertTeam
            {
                TourId = tour.Id,
                Buyer = _passengers[0].Person,
                Passengers = _passengers.GetRange(1, 1)
            });

            var res = (TeamPassengers)MockService.Get(new GetPersonsOfTeam { TeamId = tm.Id });
            res.Passengers.Should().NotBeNull();
        }

        public void CreateTeam()
        {
            Db.Insert(person_1);
            Db.Insert(person_2);
            Db.Insert(person_3);
            Db.Insert(tour);
        }
    }
}
