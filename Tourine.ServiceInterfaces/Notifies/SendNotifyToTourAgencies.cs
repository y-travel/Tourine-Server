using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Notifies
{
    [Route("/notify/agency/tour/{TourId}/role/{Role}", "POST")]
    public class SendNotifyToTourAgencies : IReturn
    {
        public Guid TourId { get; set; }
        public Role Role { get; set; }
        public string Msg { get; set; }
    }
}

