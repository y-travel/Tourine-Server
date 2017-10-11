using System;
using ServiceStack;

namespace Tourine.Models
{
    [Route("/test/{Id}","GET")]
    public class User
    {
        public int Id { get; set; }
    }
}
