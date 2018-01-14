using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/", "POST")]
    public class PostPassenger : IReturn
    {
        public Passenger Passenger { get; set; }
    }
}