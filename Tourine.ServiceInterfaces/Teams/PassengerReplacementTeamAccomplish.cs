using System;
using System.Collections.Generic;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/tours/{OldTourId}/teams/list","PUT")]
    public class PassengerReplacementTeamAccomplish : IReturnVoid
    {
        public Guid OldTourId { get; set; }
        public List<Team> Teams { get; set; }
    }
}
