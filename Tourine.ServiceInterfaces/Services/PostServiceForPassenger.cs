using ServiceStack;

namespace Tourine.ServiceInterfaces.Services
{
    [Route("/service", "POST")]
    public class PostServiceForPassenger : IReturn<Service>
    {
        public Service Service { get; set; }
    }
}
