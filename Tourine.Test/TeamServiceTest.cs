using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class TeamServiceTest : ServiceTest<TeamService>
    {
        private readonly Guid _testTeamGuid = Guid.NewGuid();
        private readonly Guid _testTourGuid = Guid.NewGuid();
        private readonly Guid _testPassengerGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateTeam();
            AppHost.Session = new AuthSession
            {
                TestMode = true,
                User = new User { Id = Guid.NewGuid() }
            };
        }

        [Test]
        public void CreateTeam_shoud_return_result()
        {
            var item = Client.Post(new CreateTeam
            {
                Team = new Team
                {
                    TourId = _testTourGuid,
                    SubmitDate = DateTime.Now,
                    LeaderId = _testPassengerGuid,
                    BuyerId = _testPassengerGuid,
                    Count = 3
                }
            });
            item.TourId.Should().Be(_testTourGuid);
            item.LeaderId.Should().Be(_testPassengerGuid);
            item.BuyerId.Should().Be(_testPassengerGuid);
        }

        [Test]
        public void UpdateTeam_should_not_throw_exception()
        {
            Client.Invoking(t => t.Put(new UpdateTeam
            {
                Team = new Team
                {
                    Id = _testTeamGuid,
                    TourId = Guid.NewGuid()
                }
            }))
            .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void UpdateTeam_should_throw_exception()
        {
            Client.Invoking(t => t.Put(new UpdateTeam
                {
                    Team = new Team
                    {
                        Id = Guid.NewGuid(),
                        TourId = Guid.NewGuid()
                    }
                }))
                .ShouldThrow<WebServiceException>();
        }

        public void CreateTeam()
        {
            Db.Insert(new Team
            {
                Id = _testTeamGuid,
                TourId = _testTourGuid,
                BuyerId = _testPassengerGuid,
                LeaderId = _testPassengerGuid,
                SubmitDate = DateTime.Today
            });
        }
    }
}
