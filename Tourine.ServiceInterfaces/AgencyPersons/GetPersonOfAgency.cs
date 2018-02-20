using System;
using ServiceStack;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.AgencyPersons
{
    [Route("/agencies/persons/", "GET")]
    public class GetPersonOfAgency : QueryDb<Person> , IJoin<Person,AgencyPerson>
    {
        [Ignore]
        public Guid? AgencyId { get; set; }
    }
}
