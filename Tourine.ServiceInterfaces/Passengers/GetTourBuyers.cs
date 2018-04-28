using System;
using System.Collections.Generic;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/tours/{TourId}/buyers","GET")]
    public class GetTourBuyers: IReturn<IList<TourBuyer>>
    {
        public Guid TourId { get; set; }
    }
}
