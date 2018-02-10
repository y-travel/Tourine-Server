using ServiceStack;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/team", "PUT")]
    public class UpdateTeam : IReturn
    {
        public Team Team { get; set; }
    }
}
