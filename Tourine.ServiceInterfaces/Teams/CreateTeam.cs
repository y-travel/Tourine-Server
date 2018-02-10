using ServiceStack;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/team", "POST")]
    public class CreateTeam : IReturn<Team>
    {
        public Team Team { get; set; }
    }
}
