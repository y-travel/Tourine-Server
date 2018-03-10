using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
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
        public object Put(UpdateTour updateTour)
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
            return Db.SingleById<Tour>(updateTour.Tour.Id);
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

        [Authenticate]
        public object Get(GetTourFreespace tour)
        {
            if (Db.Exists<Tour>(x => x.Id == tour.TourId))
            {
                //@TODO: read from database
                return "500";
            }
            throw HttpError.NotFound("");
        }

        [Authenticate]
        public object Post(ReserveBlock block)
        {
            if (!Db.Exists<Tour>(x => x.Id == block.ParentId))
                throw HttpError.NotFound("");
            if (!Db.Exists<Agency>(x => x.Id == block.AgencyId))
                throw HttpError.NotFound("");

            var tour = Db.SingleById<Tour>(block.ParentId);

            var newBlock = new Tour
            {
                AgencyId = block.AgencyId,
                BasePrice = block.BasePrice,
                Capacity = block.Capacity,
                InfantPrice = block.InfantPrice,
                ParentId = block.ParentId,
                TourDetailId = tour.TourDetailId,
                Status = TourStatus.Created,
            };

            //@TODO limited and unlimited inserted manually, should be read from parent tour
            var roomOption = new TourOption
            {
                TourId = newBlock.Id,
                OptionType = OptionType.Room,
                Price = block.RoomPrice,
                Status = OptionStatus.Limited
            };
            var busOoption = new TourOption
            {
                TourId = newBlock.Id,
                OptionType = OptionType.Bus,
                Price = block.BusPrice,
                Status = OptionStatus.Limited
            };
            var foodOption = new TourOption
            {
                TourId = newBlock.Id,
                OptionType = OptionType.Food,
                Price = block.FoodPrice,
                Status = OptionStatus.Unlimited
            };

            Db.Insert(newBlock);
            Db.Insert(roomOption);
            Db.Insert(busOoption);
            Db.Insert(foodOption);

            return Db.SingleById<Tour>(newBlock.Id);
        }

        [Authenticate]
        public object Put(UpdateBlock block)
        {
            if (!Db.Exists<Tour>(x => x.Id == block.ParentId))
                throw HttpError.NotFound("");
            if (!Db.Exists<Agency>(x => x.Id == block.AgencyId))
                throw HttpError.NotFound("");

            Db.UpdateOnly(new Tour
            {
                Id = block.Id,
                AgencyId = block.AgencyId,
                BasePrice = block.BasePrice,
                Capacity = block.Capacity,
                InfantPrice = block.InfantPrice,
            }
                , onlyFields: tour => new
                {
                    tour.Capacity,
                    tour.BasePrice,
                    tour.InfantPrice,
                    tour.AgencyId
                }
                , @where: tour => tour.Id == block.Id);

            Db.UpdateOnly(new TourOption
            {
                Price = block.BusPrice,
            },  onlyFields: option => new
            {
                option.Price
            }
            ,where: p => p.TourId == block.Id && p.OptionType == OptionType.Bus);

            Db.UpdateOnly(new TourOption
                {
                    Price = block.RoomPrice,
                }, onlyFields: option => new
                {
                    option.Price
                }
                , where: p => p.TourId == block.Id && p.OptionType == OptionType.Room);

            Db.UpdateOnly(new TourOption
                {
                    Price = block.FoodPrice,
                }, onlyFields: option => new
                {
                    option.Price
                }
                , where: p => p.TourId == block.Id && p.OptionType == OptionType.Food);

            return Db.SingleById<Tour>(block.Id);
        }
    }
}


