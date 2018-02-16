using ServiceStack;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    [Route("/persons/team", "PUT")]
    public class ChangePersonsTeam : IReturn
    {
        public TeamPerson TeamPerson { get; set; }
    }
}
