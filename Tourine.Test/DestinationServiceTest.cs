using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Destinations;

namespace Tourine.Test
{
    public class DestinationServiceTest : ServiceTest
    {
        private readonly Guid _testDestGuid = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreateDestination();
        }

        [Test]
        public void GetDestinations_should_return_result()
        {
            var res = Client.Get(new GetDestinations());
            res.Results.Count.Should().Be(1);
        }

        public void CreateDestination()
        {
            Db.Insert(new Destination { Id = _testDestGuid, Name = "Karbala" });
        }
    }
}
