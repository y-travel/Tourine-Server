using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
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

            return tour.Id == Guid.Empty
                ? tour.IsBlock ? tour.CreateBlock(Db) : tour.Create(Db, Session)
                : tour.IsBlock ? tour.UpdateBlock(Db) : tour.Update(Db, Session);
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
                .Where(tour => tour.ParentId == blocks.TourId && tour.Status != TourStatus.Creating);
            var results = AutoQuery.Execute(blocks, query);
            var mainTour = Db.SingleById<Tour>(blocks.TourId);
            results.Results.Insert(0, mainTour);
            results.Total = results.Results.Count;
            return results;
        }

        [Authenticate]
        public object Get(GetTourFreeSpace req)
        {
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound(ErrorCode.TourNotFound.ToString());
            return Db.SingleById<Tour>(req.TourId).FreeSpace.ToString();
        }

        [Authenticate]
        public void Delete(DeleteTour req)
        {
            var tour = Db.SingleById<Tour>(req.Id);
            if (tour == null)
                throw HttpError.NotFound("");
            if (tour.IsDeleteable(Db))
                tour.Delete(Db);
        }

        [Authenticate]
        public object Get(GetPersonsOfTour tour)
        {
            var reqTour = Db.LoadSingleById<Tour>(tour.TourId);
            var tours = Db.From<Tour>().Where(t => t.Id == tour.TourId || t.ParentId == tour.TourId);
            var q = Db.From<Person, PassengerList>((p, pl) => p.Id == pl.PersonId)
                .Where<PassengerList>(pl => Sql.In(pl.TourId, Db.Select(tours).Select(t => t.Id)))
                .GroupBy<Person, PassengerList>((x, pl) => new
                {
                    x,
                    pl.TourId,
                    pl.PassportDelivered,
                    VisaDelivered = pl.HaveVisa,
                    pl.TeamId,
                    pl.OptionType,
                })
                .OrderBy<PassengerList>(pl => pl.TeamId)
                .OrderBy(p => new { p.Family, p.Name })
                .Select<Person, PassengerList>((x, pl) => new
                {
                    x,
                    pl.TourId,
                    pl.PassportDelivered,
                    VisaDelivered = pl.HaveVisa,
                    pl.TeamId,
                    SumOptionType = pl.OptionType,
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
                    PassportDelivered = item.PassportDelivered,
                    TourId = item.TourId,
                    TeamId = item.TeamId,
                };
                teams.Add(t);
            }
            var leader = Db.Single(Db.From<Person, TourDetail>((p, td) => td.Id == mainTour.TourDetailId && p.Id == td.LeaderId));

            var tourPassengers = new TourPassengers { Tour = reqTour, Leader = leader, Passengers = teams };
            return tourPassengers;
        }

        [Authenticate]
        public object Get(GetTourAgency tour)
        {
            return tour.LoadChild ? Db.LoadSelect(Db.From<Tour>()
                 .Where(t => t.Id == tour.TourId || t.ParentId == tour.TourId)
                 .Select(t => new { t.Id, t.AgencyId })) :
              Db.LoadSelect(Db.From<Tour>()
                 .Where(t => t.Id == tour.TourId)
                 .Select(t => new { t.Id, t.AgencyId }));
        }

        [Authenticate]
        public void Put(PassengerReplacementTourAccomplish tour)
        {
            using (var dbTrans = Db.OpenTransaction())
            {
                if (!Db.Exists<Tour>(x => x.Id == tour.TourId))
                    throw HttpError.NotFound("");
                Db.UpdateOnly(new Tour
                {
                    InfantPrice = tour.InfantPrice,
                    BasePrice = tour.BasePrice,
                    Status = TourStatus.Created,
                }, onlyFields: t => new { t.InfantPrice, t.BasePrice, t.Status, }
                    , where: p => p.Id == tour.TourId);

                Db.UpdateOnly(new TourOption { Price = tour.BusPrice },
                    onlyFields: t => new { t.Price },
                    where: p => p.Id == tour.TourId && p.OptionType == OptionType.Bus);

                Db.UpdateOnly(new TourOption { Price = tour.RoomPrice },
                    onlyFields: t => new { t.Price },
                    where: p => p.Id == tour.TourId && p.OptionType == OptionType.Room);

                Db.UpdateOnly(new TourOption { Price = tour.FoodPrice },
                    onlyFields: t => new { t.Price },
                    where: p => p.Id == tour.TourId && p.OptionType == OptionType.Food);

                var replacedPerson = Db.Select(Db.From<PassengerList>().Where(x => x.TourId == tour.TourId));

                Db.UpdateOnly(new Team { IsPending = false },
                    onlyFields: t => new { t.IsPending },
                    where: p => Sql.In(p.Id, replacedPerson.Map(t => t.TeamId)));

                Db.Delete<PassengerList>(x => Sql.In(x.PersonId, replacedPerson.Map(pl => pl.PersonId)) && x.TourId == tour.OldTourId);
                dbTrans.Commit();
            }
        }
    }

    public class TourPassengers
    {
        public Tour Tour { get; set; }
        public Person Leader { get; set; }
        public List<TeamMember> Passengers { get; set; }
    }
}


