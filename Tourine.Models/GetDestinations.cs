using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.Models
{
    [Route("/customer/destinations", "GET")]
    public class GetDestinations:QueryDb<Destination>
    {
    }
}
