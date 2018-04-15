using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    public class Tour
    {
        [NotPopulate]
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Capacity { get; set; }

        public long BasePrice { get; set; }

        [References(typeof(Tour))]
        public Guid? ParentId { get; set; }

        [Reference]
        public Tour Parent { get; set; }

        [NotPopulate]
        public string Code { get; set; }

        [NotPopulate]
        public TourStatus Status { get; set; } = TourStatus.Created;//@TODO set to creating

        [NotPopulate]
        [References(typeof(TourDetail))]
        public Guid? TourDetailId { get; set; }

        [Reference]
        public TourDetail TourDetail { get; set; }

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        public long InfantPrice { get; set; }

        [NotPopulate]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Ignore]
        public List<TourOption> Options { get; set; }

        [Ignore]
        public bool IsBlock => ParentId != null && ParentId != Guid.Empty;

    }
}
