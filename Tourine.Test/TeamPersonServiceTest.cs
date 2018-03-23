using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Users;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class TeamPersonServiceTest : ServiceTest<TeamPersonService>
    {
        private readonly Guid _testTeamGuid = Guid.NewGuid();
        private readonly Guid _testPersonGuid = Guid.NewGuid();
        private readonly Guid _testTeamPersonGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateTeamPerson();
        }

        [Test]
        public void AddPassengerToTeam_should_not_throw_exception()
        {
            Client.Invoking(t => t.Post(new AddPersonToTeam
            {
                TeamPerson = new TeamPerson
                {
                    PersonId = _testPersonGuid,
                    TeamId = _testTeamGuid
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void AddPassengerToTeam_should_throw_exception()
        {
            Client.Invoking(t => t.Post(new AddPersonToTeam
            {
                TeamPerson = new TeamPerson
                {
                    PersonId = Guid.NewGuid(),
                    TeamId = _testTeamGuid
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void ChangePassengerTeam_should_not_throw_exception()
        {
            Client.Invoking(t => t.Put(new ChangePersonsTeam
            {
                TeamPerson = new TeamPerson
                {
                    Id = _testTeamPersonGuid,
                    PersonId = _testPersonGuid,
                    TeamId = _testTeamGuid
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void ChangePassengerTeam_should_throw_exception()
        {
            Client.Invoking(t => t.Put(new ChangePersonsTeam
            {
                TeamPerson = new TeamPerson
                {
                    Id = _testTeamPersonGuid,
                    PersonId = Guid.NewGuid(),
                    TeamId = _testTeamGuid
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetPassengerOfTeam_shuld_return_result()
        {
            var passengers = Client.Get(new GetPersonsOfTeam { TeamId = _testTeamGuid });
            passengers.Count.Should().Be(1);
        }

        public void CreateTeamPerson()
        {
            Db.Insert(new Person
            {
                Id = _testPersonGuid,
                Name = "aii",
                Family = "mirza",
                Gender = true,
                MobileNumber = "09125412131",
                Type = PersonType.Passenger,
                NationalCode = "1236547855"
            });
            Db.Insert(new Team
            {
                Id = _testTeamGuid,
                TourId = Guid.NewGuid(),
                SubmitDate = DateTime.Now
            });
            Db.Insert(new TeamPerson
            {
                Id = _testTeamPersonGuid,
                PersonId = _testPersonGuid,
                TeamId = _testTeamGuid
            });
        }
    }
}
