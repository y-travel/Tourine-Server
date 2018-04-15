using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/tours/{TourId}/passengers/toTour/{DestTourId}/{AgencyId}","POST")]
    public class PassengerReplacement : IReturn<TourTeammember>
    {
        public Guid TourId { get; set; }
        public Guid DestTourId { get; set; }
        public Guid AgencyId { get; set; }
        public List<TeamMember> Passengers { get; set; }
    }
}
