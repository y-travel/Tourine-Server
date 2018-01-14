using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/{Id}", "DELETE")]
    public class DeletePassenger : IReturn
    {
        public Guid Id { get; set; }
    }
}
