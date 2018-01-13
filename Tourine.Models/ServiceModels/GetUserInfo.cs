using System;
using ServiceStack;

namespace Tourine.Models.ServiceModels
{
    [Route("/customer/user/{Id}", "GET")]
    public class GetUserInfo : IGet
    {
        public Guid Id { get; set; }
    }
}
