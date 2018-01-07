using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.Models
{
    [Route("/customer/user/{Id}", "GET")]
    public class GetUserInfo : IGet
    {
        public Guid Id { get; set; }

    }
}
