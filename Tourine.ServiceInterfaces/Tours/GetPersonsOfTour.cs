using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/persons/", "GET")]
    public class GetPersonsOfTour : IReturn<TourPassengers>
    {
        public Guid TourId { get; set; }
    }
}
