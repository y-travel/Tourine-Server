using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passenger/{NationalCode}", "GET")]
    public class GetPassengerWithNatioanCode : IReturn<Passenger>
    {
        public string NationalCode { get; set; }
    }
}
