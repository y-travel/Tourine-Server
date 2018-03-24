using System;
using System.Collections.Generic;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/teams/{TeamId}/persons/", "GET")]
    public class GetPersonsOfTeam : IReturn<IList<TeamMember>>
    {
        public Guid TeamId { get; set; }
    }
}
