using System;
using ServiceStack;

namespace Tourine.Models
{
    [Route("/customer/tours","GET")]
    public class GetTours:QueryDb<Tour,TourInfo>
    {
        public string Code { get; set; }
    }
}
