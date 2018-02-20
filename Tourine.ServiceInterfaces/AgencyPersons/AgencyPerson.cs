using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.AgencyPersons
{
    public class AgencyPerson
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        [References(typeof(Person))]
        public Guid PersonId { get; set; }
        [Reference]
        public Person Person { get; set; }
    }
}
