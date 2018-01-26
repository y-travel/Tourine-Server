using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Blocks;

namespace Tourine.Test
{
    public class BlockServiceTest : ServiceTest
    {
        private readonly Guid _testGuid = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreateBlock();
        }

        [Test]
        public void GetBlocks_should_return_result()
        {
            var res = Client.Get(new GetBlocks());
            res.Results.Count.Should().Be(1);
        }

        [Test]
        public void PostBlock_should_not_throw_exception()
        {
            Client.Invoking(x => x.Post(new PostBlock
            {
                Block = new Block
                { 
                    TourId = Guid.NewGuid(),
                    CustomerId = Guid.NewGuid(),
                    Price = 120000,
                    Code = "1",
                    SubmitDate = DateTime.Now
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        [Test]
        public void PostBlock_should_throw_exception()
        {
            Client.Invoking(x => x.Post(new PostBlock
            {
                Block = new Block
                {
                    Id = _testGuid,
                    CustomerId = Guid.NewGuid(),
                    TourId = Guid.NewGuid()
                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutBlockValidator_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new PutBlock
            {
                Block = new Block
                {
                    Id = Guid.NewGuid(),
                    Code = "132",
                    CustomerId = Guid.NewGuid(),
                    SubmitDate = DateTime.Today,
                    TourId = Guid.NewGuid(),
                    Price = 12000

                }
            })).ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutBlockValidator_should_not_throw_exception()
        {
            Client.Invoking(x => x.Put(new PutBlock
            {
                Block = new Block
                {
                    Id = _testGuid,
                    Code = "132",
                    CustomerId = Guid.NewGuid(),
                    SubmitDate = DateTime.Today,
                    TourId = Guid.NewGuid(),
                    Price = 12000
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        private void CreateBlock()
        {
            Db.Insert(new Block
            {
                Id = _testGuid,
                Code = "1000",
                CustomerId = Guid.NewGuid(),
                Price = 200000,
                SubmitDate = DateTime.Now,
                TourId = Guid.NewGuid()
            });
        }
    }
}
