using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/persons/", "GET")]
    public class GetPersonsOfTour : IReturn<TourPassengers>
    {
        public Guid TourId { get; set; }
    }
}
