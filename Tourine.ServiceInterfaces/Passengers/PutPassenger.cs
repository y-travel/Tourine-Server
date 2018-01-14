using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/", "PUT")]
    public class PutPassenger : IReturn
    {
        public Passenger Passenger { get; set; }
    }
}