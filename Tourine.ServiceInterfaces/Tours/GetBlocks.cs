using System;
using System.ComponentModel;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/blocks", "GET")]
    public class GetBlocks : QueryDb<Tour>
    {
        [QueryDbField(Field = "Tour.ParentId")]
        public Guid TourId { get; set; }
    }
}
