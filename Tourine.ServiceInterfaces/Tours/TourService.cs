using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Persons;
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
        public object Get(GetTourOptions getTourOptions)
        {
            if (getTourOptions.TourId == Guid.Empty)
                throw HttpError.NotFound("");
            return AutoQuery.Execute(
                getTourOptions,
                AutoQuery.CreateQuery(getTourOptions, Request)
            );
        }

        [Authenticate]
        [RequiredRole(nameof(Role.Admin))]
        public object Post(UpsertTour upsertTour)
        {
            var tour = upsertTour.ConvertTo<Tour>();
            return tour.Id == Guid.Empty ? tour.Create(Db, Session) : tour.Update(Db, Session);
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
        public object Get(GetTourFreeSpace getTourFreeSpace)
        {
            if (Db.Exists<Tour>(x => x.Id == getTourFreeSpace.TourId))
            {
                var tour = Db.SingleById<Tour>(getTourFreeSpace.TourId);
                var passengerCount = tour.getCurrentPassengerCount(Db);
                var tourBlocksCapacity = tour.getBlocksCapacity(Db);
                return (tour.Capacity - tourBlocksCapacity - passengerCount).ToString();
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

            if (parentTour.Capacity - tourReserved - tourPassengers + oldBlock.Capacity < block.Capacity)
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
            var tour = Db.SingleById<Tour>(req.Id);
            if (tour == null)
                throw HttpError.NotFound("");
            tour.Delete(Db);
        }

        [Authenticate]
        public object Get(GetPersonsOfTour tour)
        {
            var tours =  Db.From<Tour>().Where(t => t.Id == tour.TourId || t.ParentId == tour.TourId);
            var q = Db.From<Person, PassengerList>((p, pl) => p.Id == pl.PersonId).Where<PassengerList>(pl => Sql.In(pl.TourId,Db.Select(tours).Select(t => t.Id)))
                .GroupBy<Person, PassengerList>((x, pl) => new
                {
                    x,
                    pl.PassportDelivered,
                    VisaDelivered = pl.HaveVisa,
                    pl.TeamId,
                })
                .OrderBy(p => p.Id)
                .Select<Person, PassengerList>((x, pl) => new
                {
                    x,
                    pl.PassportDelivered,
                    VisaDelivered = pl.HaveVisa,
                    pl.TeamId,
                    SumOptionType = Sql.Sum(nameof(PassengerList) + "." + nameof(PassengerList.OptionType)),
                });

            var items = Db.Select<TempPerson>(q);

            var mainTour = Db.Single<Tour>(x => x.Id == tour.TourId);

            var teams = new List<TeamMember>();

            foreach (var item in items)
            {
                var t = new TeamMember
                {
                    Person = item.ConvertTo<Person>(),
                    PersonId = item.Id,
                    PersonIncomes = item.SumOptionType.GetListOfTypes(),
                    HaveVisa = item.VisaDelivered,
                    PassportDelivered = item.PassportDelivered
                };
                teams.Add(t);
            }
            var leader = Db.Single(Db.From<Person, TourDetail>((p, td) => td.Id == mainTour.TourDetailId && p.Id == td.LeaderId));

            var tourPassengers = new TourPassengers { Leader = leader, Passengers = teams };
            return tourPassengers;
        }
    }

    public class TourPassengers
    {
        public Person Leader { get; set; }
        public List<TeamMember> Passengers { get; set; }
    }
}


