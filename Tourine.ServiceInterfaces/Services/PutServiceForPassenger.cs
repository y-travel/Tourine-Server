using ServiceStack;

namespace Tourine.ServiceInterfaces.Services
{
    [Route("/service", "PUT")]
    public class PutServiceForPassenger : IReturn
    {
        public Service Service { get; set; }
    }
}
