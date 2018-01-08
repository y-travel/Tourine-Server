using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.Messaging.Rcon;
using ServiceStack.OrmLite;
using Tourine.Models;

namespace Tourine.Test
{
    public class DestinationServiceTest : ServiceTest
    {
        private Guid _testDestGuid = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            createDestination();
        }

        [Test]
        public void GetDestinations_should_return_result()
        {
            var res = Client.Get(new GetDestinations());
            res.Results.Count.Should().Be(1);
        }

        public void createDestination()
        {
            Db.Insert(new Destination {Id = _testDestGuid, Name = "Karbala"});
        }
    }
}
