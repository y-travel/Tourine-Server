using System.Collections.Generic;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.Services
{
    public class PassengerServiceInfo
    {
        public List<Person> Services { get; set; }
        public Person Person { get; set; }
    }
}
