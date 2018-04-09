using System;
using System.Collections.Generic;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/agencies/{LoadChilds}","GET")]
    public class GetTourAgency : IReturn<IList<Tour>>
    {
        public Guid TourId { get; set; }
        public bool LoadChild { get; set; } = false;
    }
}
