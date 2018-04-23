using System.Collections.Generic;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class Ticket
    {
        public Tour Tour { get; set; }
        public Person Leader { get; set; }
        public List<Person> Passengers { get; set; }
    }
}
