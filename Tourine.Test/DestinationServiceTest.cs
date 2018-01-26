using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
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

        [Test]
        public void PostDestination_should_throw_exception()
        {
            Client.Invoking(d => d.Post(new PostDestination
            {
                Destination = new Destination
                {
                    Name = "1"
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PostDestination_should_not_throw_exception()
        {
            Client.Invoking(d => d.Post(new PostDestination
            {
                Destination = new Destination
                {
                    Name = "123"
                }
            })).ShouldNotThrow();
        }

        [Test]
        public void PutDestination_should_not_throw_exception()
        {
            Client.Invoking(d => d.Put(new PutDestination
            {
                Destination = new Destination
                {
                    Id = _testDestGuid,
                    Name = "123"
                }
            })).ShouldNotThrow();
        }

        [Test]
        public void PutDestination_should_throw_exception()
        {
            Client.Invoking(d => d.Put(new PutDestination
            {
                Destination = new Destination
                {
                    Id = Guid.NewGuid(),
                    Name = "33"
                }
            })).ShouldThrow<WebServiceException>();
        }

        public void CreateDestination()
        {
            Db.Insert(new Destination { Id = _testDestGuid, Name = "Karbala" });
        }
    }
}
