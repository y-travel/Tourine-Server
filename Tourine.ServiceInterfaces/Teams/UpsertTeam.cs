using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Services;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/tours/{TourId}/teams/{TeamId}", "PUT")]
    [Route("/tours/{TourId}/teams", "POST")]
    public class UpsertTeam : IReturn<Team>
    {
        public Guid? TeamId { get; set; }
        public Guid TourId { get; set; }
        public TeamMember Buyer { get; set; }
        public List<TeamMember> Passengers { get; set; }
    }
}
 