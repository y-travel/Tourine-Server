using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/", "GET")]
    [Route("/persons/{id}", "GET")]
    public class GetPersons : QueryDb<Person>
    {
        public Guid? Id { get; set; }
    }
}