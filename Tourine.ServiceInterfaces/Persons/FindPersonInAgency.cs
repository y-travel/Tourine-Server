using System;
using ServiceStack;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/{AgencyId}/{Str}", "GET")]
    public class FindPersonInAgency : QueryDb<Person> , IJoin<Person, TeamPerson, Team, Tour>
    {
        public Guid AgencyId { get; set; }
        public string Str { get; set; }
    }
}
