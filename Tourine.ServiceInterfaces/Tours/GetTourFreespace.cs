using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Web;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tours/{TourId}/freespace","GET")]
    public class GetTourFreeSpace : IReturn<string>
    {
        [QueryDbField(Field = "Id")]
        public Guid TourId { get; set; }
    }
}
