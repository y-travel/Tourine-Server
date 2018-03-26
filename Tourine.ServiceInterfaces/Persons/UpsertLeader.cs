using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/leaders", "POST")]
    public class UpsertLeader : IReturn<Person>
    {
        public Person Person { get; set; }
    }
}