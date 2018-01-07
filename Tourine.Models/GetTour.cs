using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.Models
{
    [Route("/customer/tour/{Id}","GET")]
    public class GetTour:IGet
    {
        public Guid Id { get; set; }
    }
}
