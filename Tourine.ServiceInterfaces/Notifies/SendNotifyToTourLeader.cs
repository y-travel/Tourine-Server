using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Notifies
{
    [Route("/notify/tour/{TourId}/leader", "POST")]
    public class SendNotifyToTourLeader : IReturn
    {
        public Guid TourId { get; set; }
        public string Msg { get; set; }
    }
}