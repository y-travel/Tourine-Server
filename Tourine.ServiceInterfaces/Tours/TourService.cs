using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    public class TourService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Get(GetTour reqTour)
        {
            if (!Db.Exists<Tour>(new { Id = reqTour.Id }))
                throw HttpError.NotFound("");
            var tour = Db.SingleById<Tour>(reqTour.Id);
            Db.LoadReferences(tour);
            return tour;
        }

        [Authenticate]
        public object Get(GetTours reqTours)
        {
            var query = AutoQuery.CreateQuery(reqTours, Request)
                .Where(tour => tour.AgencyId == Session.Agency.Id)
                .OrderByDescending<TourDetail>(td => td.CreationDate);
            return AutoQuery.Execute(reqTours, query);
        }

        [Authenticate]
        [RequiredRole(nameof(Role.Admin))]
        public object Post(CreateTour createReq)
        {
            var tour = new Tour
            {
                AgencyId = Session.Agency.Id,
                BasePrice = createReq.BasePrice,
                Capacity = createReq.Capacity,
                TourDetail = createReq.TourDetail
            };
            Db.Insert(tour);
            Db.SaveAllReferences(tour);
            return Db.LoadSingleById<Tour>(tour.Id);
        }

        [Authenticate]
        [RequiredRole(nameof(Role.Admin))]
        public void Put(UpdateTour updateTour)
        {
            if (!Db.Exists<Tour>(new { Id = updateTour.Tour.Id }))
                throw HttpError.NotFound("");
            Db.UpdateOnly(new Tour
            {
                Capacity = updateTour.Tour.Capacity,
                BasePrice = updateTour.Tour.BasePrice,
                ParentId = updateTour.Tour.ParentId,
                Code = updateTour.Tour.Code,
                Status = updateTour.Tour.Status,
                TourDetailId = updateTour.Tour.TourDetailId,
                AgencyId = updateTour.Tour.AgencyId
            }
                , onlyFields: tour => new
                {
                    tour.Capacity,
                    tour.BasePrice,
                    tour.ParentId,
                    tour.Code,
                    tour.Status,
                    tour.TourDetailId,
                    tour.AgencyId
                }
                , @where: tour => tour.Code == updateTour.Tour.Code);
        }

        [Authenticate]
        public object Get(GetRootTours tours)
        {
            var query = AutoQuery.CreateQuery(tours, Request.GetRequestParams())
                .Where(tour => tour.ParentId == null);
            return AutoQuery.Execute(tours, query);
        }
     
        [Authenticate]
        public object Get(GetBlocks blocks)
        {
            var query = AutoQuery.CreateQuery(blocks, Request.GetRequestParams())
                .Where(tour => tour.ParentId == blocks.TourId);
            var results = AutoQuery.Execute(blocks, query);
            var mainTour = Db.SingleById<Tour>(blocks.TourId);
            results.Results.Insert(0, mainTour);
            results.Total = results.Results.Count;
            return results;
        }
    }
}


