using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/options/{TourId}")]
    public class GetTourOptions : QueryDb<TourOption>
    {
        public Guid TourId { get; set; }
    }
}
