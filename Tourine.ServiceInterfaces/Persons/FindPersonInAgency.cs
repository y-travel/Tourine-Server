using System;
using ServiceStack;
using Tourine.ServiceInterfaces.AgencyPersons;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/search/{Str}/agency{AgencyId}/", "GET")]
    [Route("/persons/search/{Str}", "GET")]
    public class FindPersonInAgency : QueryDb<Person> , IJoin<Person, AgencyPerson>
    {
        public Guid? AgencyId { get; set; }
        public string Str { get; set; }
    }
}
