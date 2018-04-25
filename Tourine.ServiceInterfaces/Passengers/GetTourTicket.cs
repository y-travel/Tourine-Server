using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/tours/{TourId}/tickets")]
    public class GetTourTicket : IReturn<TourPersonReport>
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
    }
}
