using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.FluentValidation.TestHelper;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class DestinationServiceTest : ServiceTest<DestinationService>
    {
        private readonly Guid _testDestGuid = Guid.NewGuid();
        private readonly Destination _destination = new Destination();
        [SetUp]
        public new void Setup()
        {
            CreateDestination();
        }

        [Test]
        public void GetDestinations_should_return_result()
        {
            var results = (QueryResponse<Destination>)MockService.Get(new GetDestinations { Id = _destination.Id });
            results.Results.Count.Should().Be(1);
            results.Results[0].ShouldBeEquivalentTo(_destination);
        }

        [Test]
        public void GetDestinations_should_return_results()
        {
            var results = (QueryResponse<Destination>)MockService.Get(new GetDestinations());
            results.Results.Count.Should().Be(1);
            results.Results[0].ShouldBeEquivalentTo(_destination);
        }

        [Test]
        public void UpdateDestination_should_not_throw_exception()
        {
            new Action(() => MockService.Put(new UpdateDestination{ Destination = _destination}))
                .ShouldNotThrow<HttpError>();
        }

        [Test]
        public void UpdateDestination_should_throw_exception()
        {
            _destination.Id = Guid.NewGuid();
            new Action(() => MockService.Put(new UpdateDestination { Destination = _destination }))
                .ShouldThrow<HttpError>();
        }

        public void CreateDestination()
        {
            InsertDb(_destination);
        }
    }

    public class DestinationValidationTest
    {
        [Test]
        public void CreateDestinationValidator_should_throw_error()
        {
            var destination = new CreateDestinationValidator();
            destination.ShouldHaveValidationErrorFor(x => x.Destination.Name, "x");
        }

        [Test]
        public void CreateDestinationValidator_should_not_throw_error()
        {
            var destination = new CreateDestinationValidator();
            destination.ShouldNotHaveValidationErrorFor(x => x.Destination.Name, "any");
        }
    }
}
