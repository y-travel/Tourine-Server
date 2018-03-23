using System;
using ServiceStack.DataAnnotations;

namespace Tourine.ServiceInterfaces.Tours
{
    public class TourOption
    {
        [NotPopulate]
        public Guid Id { get; set; } = Guid.NewGuid();
        public OptionType OptionType { get; set; }
        public long Price { get; set; }
        public OptionStatus OptionStatus { get; set; }
        [References(typeof(Tour))]
        public Guid TourId { get; set; }
    }

    public enum OptionStatus
    {
        Limited = 1,
        Unlimited = 2,
    }
}
