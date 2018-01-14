using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Users
{
    [Route("/user/{Id}", "GET")]
    public class GetUser : IGet
    {
        public Guid Id { get; set; }
    }
}
