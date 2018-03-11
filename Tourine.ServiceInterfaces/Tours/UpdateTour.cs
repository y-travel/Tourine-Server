using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}", "PUT")]
    public class UpdateTour : IReturn<Tour>
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
        public int Capacity { get; set; }
        public int BasePrice { get; set; }
        public int InfantPrice { get; set; }
        public List<TourOption> Options { get; set; }
        public TourDetail TourDetail { get; set; }
    }
}
