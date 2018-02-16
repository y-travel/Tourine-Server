using ServiceStack;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    [Route("/person/team","POST")]
    public class AddPersonToTeam : IReturn
    {
        public TeamPerson TeamPerson { get; set; }
    }
}
