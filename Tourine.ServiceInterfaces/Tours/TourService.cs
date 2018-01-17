using System;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Tours
{
    public class TourService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }
        public object Get(GetTour reqTour)
        {
            if (reqTour.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Tour>(new { Id = reqTour.Id }))
                throw HttpError.NotFound("");
            var tour = Db.SingleById<Tour>(reqTour.Id);
            Db.LoadReferences(tour);
            return tour;
        }

        public object Get(GetTours reqTours)
        {
            var tours = AutoQuery.CreateQuery(reqTours, Request.GetRequestParams());
            return AutoQuery.Execute(reqTours, tours);
        }

        public void Post(PostTour postReq)
        {
            postReq.Tour.Status = TourStatus.Created;
            postReq.Tour.CreationDate = DateTime.Now;
            Db.Insert(postReq.Tour);
        }

        public void Put(PutTour putTour)
        {
            if (!Db.Exists<Tour>(new { Id = putTour.Tour.Id }))
                throw HttpError.NotFound("");
            Db.UpdateOnly(new Tour
            {
                AdultCount = putTour.Tour.AdultCount,
                AdultMinPrice = putTour.Tour.AdultMinPrice,
                BusPrice = putTour.Tour.BusPrice,
                DestinationId = putTour.Tour.DestinationId,
                Duration = putTour.Tour.Duration,
                FoodPrice = putTour.Tour.FoodPrice,
                InfantCount = putTour.Tour.InfantCount,
                InfantPrice = putTour.Tour.InfantPrice,
                IsFlight = putTour.Tour.IsFlight,
                PlaceId = putTour.Tour.PlaceId,
                RoomPrice = putTour.Tour.RoomPrice,
                StartDate = putTour.Tour.StartDate
            }
            , onlyFields: tour => new
            {
                tour.AdultCount,
                tour.AdultMinPrice,
                tour.BusPrice,
                tour.DestinationId,
                tour.Duration,
                tour.FoodPrice,
                tour.InfantCount,
                tour.InfantPrice,
                tour.IsFlight,
                tour.PlaceId,
                tour.RoomPrice,
                tour.StartDate
            }
            , @where: tour => tour.Code == putTour.Tour.Code);
        }
    }
}


