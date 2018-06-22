using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.FluentValidation.TestHelper;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Models;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class PlaceServiceTest : ServiceTest<PlaceService>
    {
        private readonly Place _place = new Place();
        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            Db.Insert(_place);
        }

        [Test]
        public void GetPlace_should_return_result()
        {
            var agency = (QueryResponse<Place>)MockService.Get(new GetPlaces());
            agency.Results[0].ShouldBeEquivalentTo(_place);
        }

        [Test]
        public void PutPlace_should_throw_exception()
        {
            new Action(() => MockService.Put(new PutPlace { Place = new Place() }))
                .ShouldThrow<HttpError>();
        }

    }

    public class PlaceValidationTest
    {
        [Test]
        public void PutPlaceValidation_should_throw_error()
        {
            var validator = new PutPlaceValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Place.Name, string.Empty);
            validator.ShouldNotHaveValidationErrorFor(x => x.Place.Name, "xx");
        }

        [Test]
        public void PostPlaceValidation_should_throw_error()
        {
            var validator = new PostPlaceValidator();
            validator.ShouldHaveValidationErrorFor(x => x.Place.Name, string.Empty);
            validator.ShouldNotHaveValidationErrorFor(x => x.Place.Name, "xx");
        }
    }
}
