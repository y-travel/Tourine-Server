using ServiceStack;

namespace Tourine.ServiceInterfaces.Agencies
{
    [Route("/agencies","POST")]
    public class CreateAgency : IReturn
    {
        public Agency Agency { get; set; }
    }
}
