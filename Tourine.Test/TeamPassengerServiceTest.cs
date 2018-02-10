using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class TeamPassengerServiceTest : ServiceTest
    {
        private readonly Guid _testTeamGuid = Guid.NewGuid();
        private readonly Guid _testPassengerGuid = Guid.NewGuid();
        private readonly Guid _testTeamPassengerGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateTeamPassenger();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = Guid.NewGuid() }
            };
        }

        [Test]
        public void AddPassengerToTeam_should_not_throw_exception()
        {
            Client.Invoking(t => t.Post(new AddPassengerToTeam
            {
                TeamPassenger = new TeamPassenger
                {
                    PassengerId = _testPassengerGuid,
                    TeamId = _testTeamGuid
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddPassengerToTeam_should_throw_exception()
        {
            Client.Invoking(t => t.Post(new AddPassengerToTeam
            {
                TeamPassenger = new TeamPassenger
                {
                    PassengerId = Guid.NewGuid(),
                    TeamId = _testTeamGuid
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void ChangePassengerTeam_should_not_throw_exception()
        {
            Client.Invoking(t => t.Put(new ChangePassengerTeam
            {
                TeamPassenger = new TeamPassenger
                {
                    Id = _testTeamPassengerGuid,
                    PassengerId = _testPassengerGuid,
                    TeamId = _testTeamGuid
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void ChangePassengerTeam_should_throw_exception()
        {
            Client.Invoking(t => t.Put(new ChangePassengerTeam
            {
                TeamPassenger = new TeamPassenger
                {
                    Id = _testTeamPassengerGuid,
                    PassengerId = Guid.NewGuid(),
                    TeamId = _testTeamGuid
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetPassengerOfTeam_shuld_return_result()
        {
            var passengers = Client.Get(new GetPassengerOfTeam { TeamId = _testTeamGuid });
            passengers.Results.Count.Should().Be(1);
        }

        public void CreateTeamPassenger()
        {
            Db.Insert(new Passenger
            {
                Id = _testPassengerGuid,
                Name = "aii",
                Family = "mirza",
                Gender = true,
                MobileNumber = "09125412131",
                Type = PassengerType.Passenger,
                NationalCode = "1236547855"
            });
            Db.Insert(new Team
            {
                Id = _testTeamGuid,
                TourId = Guid.NewGuid(),
                SubmitDate = DateTime.Now
            });
            Db.Insert(new TeamPassenger
            {
                Id = _testTeamPassengerGuid,
                PassengerId = _testPassengerGuid,
                TeamId = _testTeamGuid
            });
        }
    }
}
