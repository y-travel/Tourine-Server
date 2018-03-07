using ServiceStack;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.Agencies
{
    [Route("/agencies","POST")]
    public class CreateAgency : IReturn<Agency>
    {
        public Agency Agency { get; set; }
        public Person Person { get; set; }
    }
}
