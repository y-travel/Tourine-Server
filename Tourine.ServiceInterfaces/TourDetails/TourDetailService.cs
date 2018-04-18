using System.Net;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Destinations;

namespace Tourine.ServiceInterfaces.TourDetails
{
    public class TourDetailService : AppService
    {
        [Authenticate]
        public object Get(GetTourDetail detail)
        {
            var tour = Db.SingleById<TourDetail>(detail.Id);
            if (tour == null)
                throw HttpError.NotFound("");

            return tour;
        }

    }

   
}

