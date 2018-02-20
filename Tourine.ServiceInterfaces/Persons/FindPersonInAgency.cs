using System;
using ServiceStack;
using Tourine.ServiceInterfaces.AgencyPersons;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/{AgencyId}/{Str}", "GET")]
    public class FindPersonInAgency : QueryDb<Person> , IJoin<Person, AgencyPerson>
    {
        public Guid AgencyId { get; set; }
        public string Str { get; set; }
    }
}
