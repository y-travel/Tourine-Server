using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models.DatabaseModels;
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
                    Capacity = 1,
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

        private void CreateBlock()
        {
            Db.Insert(new Block
            {
                Id = _testGuid,
                Capacity = 2,
                Code = "1000",
                CustomerId = Guid.NewGuid(),
                Price = 200000,
                SubmitDate = DateTime.Now,
                TourId = Guid.NewGuid()
            });
        }
    }
}
