using ServiceStack;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
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

