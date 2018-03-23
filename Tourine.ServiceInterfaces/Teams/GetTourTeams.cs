using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/tours/{TourId}/teams","GET")]
    public class GetTourTeams: QueryDb<Team>
    {
        public Guid TourId { get; set; }
    }
}
