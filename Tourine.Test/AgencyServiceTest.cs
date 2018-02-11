using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;

namespace Tourine.Test
{
    public class AgencyServiceTest : ServiceTest
    {
        private readonly Guid _testAgencyGuid = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreateAgency();
            AppHost.Session = new AuthSession { TestMode = true };
        }

        [Test]
        public void GetAgency_should_return_result()
        {
            var item = Client.Get(new GetAgency { Id = _testAgencyGuid });
            item.Name.Should().Be("TaHa");
        }

        [Test]
        public void GetAgency_should_throw_exception()
        {
            Client.Invoking(a => a.Get(new GetAgency { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void GetAgencies_should_return_results()
        {
            var results = Client.Get(new GetAgencies());
            results.Results.Count.Should().Be(1);
        }

        [Test]
        public void CreateAgency_should_throw_exception()
        {
            Client.Invoking(a => a.Post(new CreateAgency
            {
                Agency = new Agency
                {
                    Id = Guid.NewGuid(),
                    Name = "1",
                    PhoneNumber = "validNum"
                }
            }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void CreateAgency_should_not_throw_exception()
        {
            Client.Invoking(a => a.Post(new CreateAgency
            {
                Agency = new Agency
                {
                    Id = Guid.NewGuid(),
                    Name = "Eram",
                    PhoneNumber = "validNum"
                }
            }))
                .ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void UpdateAgency_should_throw_exception()
        {
            Client.Invoking(a => a.Put(new UpdateAgency
            {
                Agency = new Agency
                {
                    Id = Guid.NewGuid(),
                    Name = "Raja",
                    PhoneNumber = "validNum"
                }
            }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void UpdateAgency_should_not_throw_exception()
        {
            Client.Invoking(a => a.Put(new UpdateAgency
            {
                Agency = new Agency
                {
                    Id = _testAgencyGuid,
                    Name = "Raja",
                    PhoneNumber = "validNum"
                }
            }))
                .ShouldNotThrow<WebServiceException>();
        }

        private void CreateAgency()
        {
            Db.Insert(new Agency { Id = _testAgencyGuid, Name = "TaHa", PhoneNumber = "021356478" });
        }
    }
}
