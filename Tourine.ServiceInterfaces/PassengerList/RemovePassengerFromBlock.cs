using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.PassengerList
{
    [Route("/RemovePassengerFromBlock/{PId}/{BId}", "DELETE")]
    public class RemovePassengerFromBlock : IReturn
    {
        public Guid PId { get; set; }
        public Guid BId { get; set; }
    }
}
