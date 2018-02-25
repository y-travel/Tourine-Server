using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Notifies
{
    [Route("/notify/tour/{TourId}/buyers/", "POST")]
    public class SendNotifyToTourBuyers : IReturn
    {
        public Guid TourId { get; set; }
        public string Msg { get; set; }
    }
}