using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Services;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/tours/{TourId}/teams", "POST")]
    public class CreateTeam : IReturn
    {
        public Guid TourId { get; set; }
        public TeamMember Buyer { get; set; }
        public List<TeamMember> Passengers { get; set; }
    }
}
