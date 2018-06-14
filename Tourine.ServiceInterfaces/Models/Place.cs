using System;
using ServiceStack;
using ServiceStack.FluentValidation;

namespace Tourine.ServiceInterfaces.Models
{
    public class PutPlaceValidator : AbstractValidator<PutPlace>
    {
        public PutPlaceValidator()
        {
            RuleFor(p => p.Place.Id).NotEmpty();
            RuleFor(p => p.Place.Name.Length).GreaterThanOrEqualTo(2);
        }
    }

    [Route("/place", "PUT")]
    public class PutPlace
    {
        public Place Place { get; set; }
    }

    public class PostPlaceValidator : AbstractValidator<PostPlace>
    {
        public PostPlaceValidator()
        {
            RuleFor(p => p.Place.Name.Length).GreaterThanOrEqualTo(2);
        }
    }

    [Route("/post/","POST")]
    public class PostPlace
    {
        public Place Place { get; set; }
    }

    [Route("/places")]
    public class GetPlaces : QueryDb<Place>
    {
    }

    public class Place
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
