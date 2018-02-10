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
            return Db.SingleById<TourDetail>(detail.Id);
        }

        [Authenticate]
        public void Put(UpdateTourDetail detail)
        {
            if (!Db.Exists<TourDetail>(new { Id = detail.TourDetail.Id}))
                throw HttpError.NotFound("");
            if (!Db.Exists<Destination>(new { Id = detail.TourDetail.DestinationId }))
                throw HttpError.NotFound("");
            Db.Update(detail.TourDetail);
        }
    }
}
