using System;
using ServiceStack;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    public class Tour
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Capacity { get; set; }
        public int BasePrice { get; set; }

        [References(typeof(Tour))]
        public Guid? ParentId { get; set; }
        [Reference]
        public Tour Parent { get; set; }

        public string Code { get; set; }
        public TourStatus Status { get; set; } = TourStatus.Created;

        [References(typeof(TourDetail))]
        public Guid? TourDetailId { get; set; }
        [Reference]
        public TourDetail TourDetail { get; set; }

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        public long InfantPrice { get; set; }
    }
}
