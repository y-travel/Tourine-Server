using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Tourine.Models
{
    [Route("/customer/tour/","POST")]
    public class PostTour:IReturn<Tour>
    {
        public Tour Tour { get; set; }
    }
}
