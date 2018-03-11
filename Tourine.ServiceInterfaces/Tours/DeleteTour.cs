using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}","DELETE")]
    public class DeleteTour : IReturn
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
    }
}
