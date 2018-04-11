using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{Id}", "POST")]
    public class UpsertTour : IReturn<Tour>
    {
        //Shared Properties
        public Guid Id { get; set; }
        public int Capacity { get; set; }
        public int BasePrice { get; set; }
        public int InfantPrice { get; set; }
        public List<TourOption> Options { get; set; }
        public bool IsBlock { get; set; }

        //Tour Properties
        public TourDetail TourDetail { get; set; }

        //Block Properties
        public Guid? ParentId { get; set; }
        public Guid? AgencyId { get; set; }

    }
}
