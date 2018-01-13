using System;
using ServiceStack;

namespace Tourine.Models.ServiceModels
{
    [Route("/customer/tour/{Id}","GET")]
    public class GetTour:IGet
    {
        public Guid Id { get; set; }
    }
}
