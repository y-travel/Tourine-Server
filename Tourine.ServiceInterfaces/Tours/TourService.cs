using System;
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
                .Where(tour => tour.ParentId != null);
            return AutoQuery.Execute(reqTours, query);
        }

        [Authenticate]
        public object Post(PostTour postReq)
        {
            postReq.Tour.Status = TourStatus.Created;
            postReq.Tour.CreationDate = DateTime.Now;
            postReq.Tour.Id = Guid.NewGuid();
            Db.Insert(postReq.Tour);
            return Db.SingleById<Tour>(postReq.Tour.Id);
        }

        [Authenticate]
        public void Put(PutTour putTour)
        {
            if (!Db.Exists<Tour>(new { Id = putTour.Tour.Id }))
                throw HttpError.NotFound("");
            Db.UpdateOnly(new Tour
            {
                Capacity = putTour.Tour.Capacity,
                BasePrice = putTour.Tour.BasePrice,
                ParentId = putTour.Tour.ParentId,
                Code = putTour.Tour.Code,
                Status = putTour.Tour.Status,
                TourDetailId = putTour.Tour.TourDetailId,
                AgencyId = putTour.Tour.AgencyId
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
                , @where: tour => tour.Code == putTour.Tour.Code);
        }

        [Authenticate]
        public object Get(GetRootTours tours)
        {
            var query = AutoQuery.CreateQuery(tours, Request)
                .Where(tour => tour.ParentId == null);
            return AutoQuery.Execute(tours, query);
        }
    }
}


