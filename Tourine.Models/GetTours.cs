using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.Models
{

    [Route("/customer/tours", "GET")]
    public class GetTours : QueryDb<Tour>
    {

    }

}
