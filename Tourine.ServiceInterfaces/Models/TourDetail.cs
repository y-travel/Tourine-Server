using System;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.FluentValidation;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Models
{
    public class GetTourDetailValidator : AbstractValidator<GetTourDetail>
    {
        public GetTourDetailValidator()
        {
            RuleFor(t => t.Id).NotEmpty();
        }
    }

    [Route("/tourDetail/{ID}", "GET")]
    public class GetTourDetail : IReturn<TourDetail>
    {
        public Guid Id { get; set; }
    }

    public class TourDetail
    {
        [NotPopulate]
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Destination))]
        public Guid DestinationId { get; set; }
        [Reference]
        public Destination Destination { get; set; }

        public int Duration { get; set; }
        public DateTime StartDate { get; set; }

        [Ignore]
        public DateTime EndDate => StartDate.AddDays(Duration);
        [References(typeof(Place))]
        public Guid PlaceId { get; set; }
        [Reference]
        public Place Place { get; set; }

        [NotPopulate]
        public bool IsFlight { get; set; } = true;

        [References(typeof(Person))]
        public Guid? LeaderId { get; set; }
        [Reference]
        public Person Leader { get; set; }
    }
}