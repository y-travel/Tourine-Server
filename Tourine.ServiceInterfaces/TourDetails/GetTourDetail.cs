using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.TourDetails
{
    [Route("/tourDetail/{ID}", "GET")]
    public class GetTourDetail : IReturn<TourDetail>
    {
        public Guid Id { get; set; }
    }
}
