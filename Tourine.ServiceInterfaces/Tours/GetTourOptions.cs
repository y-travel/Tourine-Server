using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/options/{TourId}")]
    public class GetTourOptions : QueryDb<TourOption>
    {
        public Guid TourId { get; set; }
    }
}
