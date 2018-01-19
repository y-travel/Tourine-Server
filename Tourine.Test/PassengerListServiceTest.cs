using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Blocks;
using Tourine.ServiceInterfaces.PassengerList;
using Tourine.ServiceInterfaces.Passengers;

namespace Tourine.Test
{
    public class PassengerListServiceTest : ServiceTest
    {
        private readonly Guid _testBlockId = Guid.NewGuid();
        private readonly Guid _testPassengerId = Guid.NewGuid();

        [SetUp]
        public new void Setup()
        {
            CreatePassengerList();
        }

        private void CreatePassengerList()
        {
            Db.Insert(new Passenger { Id = _testPassengerId });
            Db.Insert(new Block { Id = _testBlockId });
        }

        [Test]
        public void AddPassengerToBlobk_shoudl_not_throw_exception()
        {
            Client.Invoking(x => x.Post(new AddPassengerToBlock
            {
                PassengerList = new PassengerList
                {
                    BlockId = Guid.NewGuid(),
                    PassengerId = Guid.NewGuid()
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void RemovePassengerFromBlock_should_not_throw_exception()
        {
            Client.Invoking(x => x.Delete(new RemovePassengerFromBlock
            {
                PId = _testPassengerId,
                BId = _testBlockId
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void RemovePassengerFromBlock_should_throw_exception()
        {
            Client.Invoking(x => x.Delete(new RemovePassengerFromBlock
            {
                PId = Guid.NewGuid(),
                BId = Guid.NewGuid()
            })).ShouldThrow<WebServiceException>();
        }
    }
}
