using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Notifies
{
    [Route("/notify/tour/{TourId}/passengers/", "POST")]
    public class SendNotifyToTourPassengers : IReturn
    {
        public Guid TourId { get; set; }
        public string Msg { get; set; }
    }
}