using System.Collections.Generic;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/tours/teams/list","PUT")]
    public class UpdateTeamList : IReturnVoid
    {
        public List<Team> Teams { get; set; }
    }
}
