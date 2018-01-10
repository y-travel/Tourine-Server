using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Models;

namespace Tourine.Test
{
    public class ReagentServiceTest : ServiceTest
    {
        private readonly Guid _testGuid = Guid.NewGuid();
        [SetUp]
        public new void Setup()
        {
            CreateReagent();
        }

        [Test]
        public void GetReagent_should_return_result()
        {
            var res = Client.Get(new GetReagent());
            res.Results.Count.Should().Be(1);
            res.Results[0].Name.Should().Be("name");
        }

        [Test]
        public void DeleteReagent_should_return_result()
        {
            var res = Client.Delete(new DeleteReagent { Id = _testGuid });
            res.Id.Should().Be(_testGuid);
        }

        [Test]
        public void DeleteReagent_should_throw_exeption()
        {
            Client.Invoking(x => x.Delete(new DeleteReagent { Id = Guid.NewGuid() }))
                .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PutReagent_should_update_data_and_return_result()
        {
            var res = Client.Put(new PutReagent
            {
                reagent = new Reagent
                {
                    Id = _testGuid,
                    Name = "emaN",
                    Family = "fdj",
                    AgencyName = "agc",
                    Phone = "021-555 369852",
                    MobileNumber = "00989125412164",
                    Email = "some@gmail.com"
                }
            });
            res.Name.Should().Be("emaN");
        }

        [Test]
        public void PutReagent_should_throw_exception()
        {
            Client.Invoking(x => x.Put(new PutReagent
            {
                reagent = new Reagent
                {
                    Id = Guid.NewGuid(),
                    Name = "emaN",
                    Family = "fdj",
                    AgencyName = "agc",
                    Phone = "021-555 369852",
                    MobileNumber = "00989125412164",
                    Email = "some@gmail.com"
                }
            }))
            .ShouldThrow<WebServiceException>();
        }

        [Test]
        public void PostReagent_should_return_result()
        {

            var res = Client.Post(new PostReagent
            {
                reagent = new Reagent
                {
                    Id = _testGuid,
                    Name = "aaaa",
                    Family = "ffff",
                    AgencyName = "gggg",
                    Phone = "01236456789",
                    MobileNumber = "009832145698",
                    Email = "email@email.com"
                }
            });
        }

        public void CreateReagent()
        {
            Db.Insert(new Reagent
            {
                Id = _testGuid,
                Name = "name",
                Family = "fam",
                AgencyName = "Yekta",
                Email = "some@test.com",
                MobileNumber = "09123456789",
                Phone = "021-555 6935"
            });
        }
    }
}
