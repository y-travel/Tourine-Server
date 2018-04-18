using System.Net;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.TourDetails
{
    public class TourDetailService : AppService
    {
        [Authenticate]
        public object Get(GetTourDetail detail)
        {
            return TourExtensions.GetTourDetail(Db, detail.Id);
        }

    }
   
}

