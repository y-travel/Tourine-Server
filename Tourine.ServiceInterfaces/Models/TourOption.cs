using System;
using ServiceStack;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.ServiceInterfaces.Models
{
    [Route("/tours/options/{TourId}")]
    public class GetTourOptions : QueryDb<TourOption>
    {
        public Guid TourId { get; set; }
    }

    public class TourOption
    {
        [NotPopulate]
        public Guid Id { get; set; } = Guid.NewGuid();
        public OptionType OptionType { get; set; }

        public long Price { get; set; }

        public OptionStatus OptionStatus { get; set; }

        [NotPopulate]
        [References(typeof(Tour))]
        public Guid TourId { get; set; }
    }

    public enum OptionStatus
    {
        Limited = 1,
        Unlimited = 2,
    }
}
