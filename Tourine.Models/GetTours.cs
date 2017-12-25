using System;
using ServiceStack;

namespace Tourine.Models
{
    [Route("/customer/tours","GET")]
    public class GetTours:QueryDb<Tour,TourInfo>,IGet
    {
        public string Code { get; set; }
    }
}
