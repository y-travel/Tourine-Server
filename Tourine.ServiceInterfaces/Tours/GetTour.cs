using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tour/{ID}", "GET")]
    public class GetTour : IGet
    {
        public Guid Id { get; set; }
    }
}
