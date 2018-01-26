using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Places;

namespace Tourine.Test
{
    public class PlaceServiceTest : ServiceTest
    {
        private readonly Guid _testGuid = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            Db.Insert(new Place
            {
                Id = _testGuid,
                Name = "newPlace"
            });
        }

        [Test]
        public void GetPlace_should_return_result()
        {
            var item = Client.Get(new GetPlaces());
            item.Results[0].Name.Should().Be("newPlace");
        }

        [Test]
        public void PutPlace_should_throw_exception()
        {
            Client.Invoking(p => p.Put(new PutPlace
            {
                Place = new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "13"
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutPlace_should_throw_validation_exception()
        {
            Client.Invoking(p => p.Put(new PutPlace
            {
                Place = new Place
                {
                    Id = _testGuid,
                    Name = "3"
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutPlace_should_not_throw_exception()
        {
            Client.Invoking(p => p.Put(new PutPlace
            {
                Place = new Place
                {
                    Id = _testGuid,
                    Name = "123"
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void PostPlace_should_throw_exception()
        {
            Client.Invoking(p => p.Post(new PostPlace
            {
                Place = new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "6"
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PostPlace_should_not_throw_exception()
        {
            Client.Invoking(p => p.Post(new PostPlace
            {
                Place = new Place
                {
                    Id = Guid.NewGuid(),
                    Name = "123456"
                }
            })).ShouldNotThrow<WebServiceException>();
        }
    }
}
