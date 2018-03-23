using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{Id}","DELETE")]
    public class DeleteTour : IReturn
    {
        public Guid Id { get; set; }
    }
}
