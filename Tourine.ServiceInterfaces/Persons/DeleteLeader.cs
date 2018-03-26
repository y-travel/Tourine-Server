using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/leaders/{Id}","DELETE")]
    public class DeleteLeader : IReturnVoid
    {
        public Guid Id { get; set; }
    }
}
