using System;
using ServiceStack;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    [Route("/team/{TeamId}/persons/", "GET")]
    public class GetPersonsOfTeam : QueryDb<Person> , IJoin<Person,TeamPerson>
    {
        public Guid TeamId { get; set; }
    }
}
