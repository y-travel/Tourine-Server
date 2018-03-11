using System;
using System.Data;
using System.Net;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Services;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
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
                .OrderByDescending(td => td.CreationDate);
            return AutoQuery.Execute(reqTours, query);
        }

        [Authenticate]
        [RequiredRole(nameof(Role.Admin))]
        public object Post(CreateTour createReq)
        {
            using (IDbTransaction dbTrans = Db.OpenTransaction())
            {
                var tour = new Tour
                {
                    AgencyId = Session.Agency.Id,
                    BasePrice = createReq.BasePrice,
                    InfantPrice = createReq.InfantPrice,
                    Capacity = createReq.Capacity,
                    TourDetail = createReq.TourDetail
                };

                Db.Insert(tour);
                Db.SaveAllReferences(tour);
                foreach (var option in createReq.Options)
                {
                    Db.Insert(new TourOption
                    {
                        OptionType = option.OptionType,
                        Price = option.Price,
                        TourId = tour.Id,
                        OptionStatus = option.OptionType.GetDefaultStatus()
                    });
                }
                dbTrans.Commit();
                return Db.LoadSingleById<Tour>(tour.Id);
            }
        }

        [Authenticate]
        [RequiredRole(nameof(Role.Admin))]
        public object Put(UpdateTour updateTour)
        {
            if (!Db.Exists<Tour>(new { Id = updateTour.TourId }))
                throw HttpError.NotFound("");
            using (IDbTransaction dbTrans = Db.OpenTransaction())
            {
                Db.UpdateOnly(new Tour
                {
                    Capacity = updateTour.Capacity,
                    BasePrice = updateTour.BasePrice,
                    InfantPrice = updateTour.InfantPrice
                }
                    , onlyFields: tour => new
                    {
                        tour.Capacity,
                        tour.BasePrice,
                        tour.InfantPrice
                    }
                    , @where: tour => tour.Id == updateTour.TourId);

                var newTour = Db.SingleById<Tour>(updateTour.TourId);

                Db.UpdateOnly(new TourDetail
                {
                    DestinationId = updateTour.TourDetail.DestinationId,
                    Duration = updateTour.TourDetail.Duration,
                    IsFlight = updateTour.TourDetail.IsFlight,
                    LeaderId = updateTour.TourDetail.LeaderId,
                    PlaceId = updateTour.TourDetail.PlaceId,
                    StartDate = updateTour.TourDetail.StartDate
                }
                    , onlyFields: tour => new
                    {
                        tour.DestinationId,
                        tour.Duration,
                        tour.IsFlight,
                        tour.LeaderId,
                        tour.PlaceId,
                        tour.StartDate
                    }
                    , @where: tourDetail => tourDetail.Id == newTour.TourDetailId);

                foreach (var option in updateTour.Options)
                {
                    Db.UpdateOnly(new TourOption
                    {
                        Price = option.Price,
                    }, onlyFields: opt => new
                    {
                        option.Price
                    }
                        , where: p => p.TourId == newTour.Id && p.OptionType == option.OptionType);
                }
                dbTrans.Commit();
                return newTour;
            }
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
                var mainTour = Db.SingleById<Tour>(tour.TourId);
                var mainTourPassengers = mainTour.getCurrentPassengerCount(Db);
                var tourReserved = mainTour.getBlocksCapacity(Db);
                return (mainTour.Capacity - tourReserved - mainTourPassengers).ToString();
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

            var tourReserved = tour.getBlocksCapacity(Db);
            var tourPassengers = tour.getCurrentPassengerCount(Db);

            if (tour.Capacity < block.Capacity + tourReserved + tourPassengers)
                throw HttpError.NotFound("freeSpace");

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
                OptionStatus = OptionType.Room.GetDefaultStatus()
            };
            var busOoption = new TourOption
            {
                TourId = newBlock.Id,
                OptionType = OptionType.Bus,
                Price = block.BusPrice,
                OptionStatus = OptionType.Bus.GetDefaultStatus()
            };
            var foodOption = new TourOption
            {
                TourId = newBlock.Id,
                OptionType = OptionType.Food,
                Price = block.FoodPrice,
                OptionStatus = OptionType.Food.GetDefaultStatus()
            };
            using (var transDb = Db.OpenTransaction())
            {
                Db.Insert(newBlock);
                Db.Insert(roomOption);
                Db.Insert(busOoption);
                Db.Insert(foodOption);
                transDb.Commit();
            }

            return Db.SingleById<Tour>(newBlock.Id);
        }

        [Authenticate]
        public object Put(UpdateBlock block)
        {
            if (!Db.Exists<Tour>(x => x.Id == block.ParentId))
                throw HttpError.NotFound("");
            if (!Db.Exists<Agency>(x => x.Id == block.AgencyId))
                throw HttpError.NotFound("");

            var parentTour = Db.SingleById<Tour>(block.ParentId);
            var oldBlock = Db.SingleById<Tour>(block.Id);
            var tourReserved = parentTour.getBlocksCapacity(Db);
            var tourPassengers = oldBlock.getCurrentPassengerCount(Db);

            if (parentTour.Capacity - tourReserved - tourPassengers - oldBlock.Capacity < block.Capacity)
                throw HttpError.NotFound("freeSpace");

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
            }, onlyFields: option => new
            {
                option.Price
            }
            , where: p => p.TourId == block.Id && p.OptionType == OptionType.Bus);

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

        [Authenticate]
        public void Delete(DeleteTour req)
        {
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound("");

            var tour = Db.SingleById<Tour>(req.TourId);
            Db.Delete<Tour>(tour.Id);
            Db.Delete(Db.From<PassengerList>().Where(p => p.TourId == tour.Id));
            var teams = Db.Select(Db.From<Team>().Where(team => team.TourId == tour.Id));
            Db.Delete(Db.From<Team>().Where(team => team.TourId == tour.Id));
            if (tour.ParentId == null)
                Db.Delete(Db.From<TourDetail>().Where(t => t.Id == tour.TourDetailId));
            foreach (var team in teams)
            {
                Db.Delete(Db.From<TeamPerson>().Where(tp => tp.TeamId == team.Id));
            }
        }
    }
}


