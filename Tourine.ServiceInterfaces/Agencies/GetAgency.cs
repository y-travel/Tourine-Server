using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Agencies
{
    [Route("/agencies/{id}", "GET")]
    public class GetAgency : IReturn<Agency>
    {
        public Guid Id { get; set; }
    }
}
