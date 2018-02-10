using ServiceStack;

namespace Tourine.ServiceInterfaces.Agencies
{
    [Route("/agencies","PUT")]
    public class UpdateAgency : IReturn
    {
        public Agency Agency { get; set; }
    }
}
