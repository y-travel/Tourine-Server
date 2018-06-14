using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/passengers/", "GET")]
    public class GetTourPassengers : IReturn<TourPassengers>
    {
        public Guid TourId { get; set; }
    }
}
