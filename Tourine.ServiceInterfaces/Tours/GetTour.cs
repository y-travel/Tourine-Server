using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{ID}", "GET")]
    public class GetTour : IGet
    {
        public Guid Id { get; set; }
    }
}
