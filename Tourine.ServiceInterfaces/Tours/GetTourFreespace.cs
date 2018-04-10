using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/freespace","GET")]
    public class GetTourFreeSpace : IReturn<string>
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
    }
}
