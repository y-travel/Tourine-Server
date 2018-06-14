using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public class PassengerListService : AppService
    {
        public IAutoQueryDb AutoQueryDb { get; set; }
        public object Post(PostServiceForPassenger forPassenger)
        {
            forPassenger.PassengerList.Id = Guid.NewGuid();
            Db.Insert(forPassenger.PassengerList);
            return Db.SingleById<PassengerList>(forPassenger.PassengerList.Id);
        }

        public void Put(PutServiceForPassenger forPassenger)
        {
            if (!Db.Exists<PassengerList>(new { Id = forPassenger.PassengerList.Id }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Person>(new { Id = forPassenger.PassengerList.PersonId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Tour>(new { Id = forPassenger.PassengerList.TourId }))
                throw HttpError.NotFound("");
            Db.Update(forPassenger.PassengerList);
        }

        public object Get(GetServiceOfTour serviceOfTour)
        {
            var query = AutoQueryDb.CreateQuery(serviceOfTour, Request.GetRequestParams());
            return AutoQueryDb.Execute(serviceOfTour, query);
        }

        public object Post(PassengerReplacement req)
        {
            var srcTour = Db.SingleById<Tour>(req.TourId);
            if (srcTour == null)
                throw HttpError.NotFound("");
            var desTour = Db.SingleById<Tour>(req.DestTourId);
            if (desTour == null)
                throw HttpError.NotFound("");

            if (req.Passengers.Count > desTour.FreeSpace)
                throw HttpError.NotFound("freeSpace");

            var newTeamList = new List<Team>();
            Tour newBlock;
            using (var transDb = Db.OpenTransaction())
            {
                newBlock = desTour.ReservePendingBlock(req.Passengers.Count, req.AgencyId, Db);
                var teamIds = req.Passengers.Map(x => x.TeamId).Distinct().ToList();
                var teams = Db.LoadSelect(Db.From<Team>().Where(t => Sql.In(t.Id, teamIds)));
                foreach (var team in teams)
                {
                    var sameTeamPassengers = req.Passengers.FindAll(t => t.TeamId == team.Id);
                    desTour.ClearPending(Db);
                    newTeamList.Add(new TeamLogic(Db).CopyPassengers(team, newBlock, sameTeamPassengers));
                }

                transDb.Commit();
            }

            var options = Db.Select(Db.From<TourOption>().Where(to => to.TourId == newBlock.Id));
            var result = new TourTeammember
            {
                isTeam = req.AgencyId == Session.Agency.Id,
                Id = newBlock.Id,
                BasePrice = newBlock.BasePrice,
                InfantPrice = newBlock.InfantPrice,
                Agency = Db.SingleById<Agency>(req.AgencyId),
                FoodPrice = options.Find(x => x.OptionType == OptionType.Food).Price,
                RoomPrice = options.Find(x => x.OptionType == OptionType.Room).Price,
                BusPrice = options.Find(x => x.OptionType == OptionType.Bus).Price,
                Teams = newTeamList,
            };
            //@TODO ughly
            return result;
        }

        [Authenticate]
        public object Get(GetTourTicket req)
        {
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound(ErrorCode.TourNotFound.ToString());

            var tours = Db.From<Tour>().Where(t => t.Id == req.TourId || t.ParentId == req.TourId);

            var tour = Db.LoadSingleById<Tour>(req.TourId);
            var leader = Db.SingleById<Person>(tour.TourDetail.LeaderId);
            var query = Db.From<Person, PassengerList>()
                .Where<PassengerList>(x => Sql.In(x.TourId, Db.Select(tours).Map(y => y.Id)))
                .SelectDistinct(x => x);
            var passengers = Db.Select(query);
            var ticket = new TourPersonReport
            {
                Tour = tour,
                Leader = leader,
                Passengers = passengers,
            };
            return ticket;
        }

        [Authenticate]
        public object Get(GetTourVisa req)
        {
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound(ErrorCode.TourNotFound.ToString());

            var tours = Db.From<Tour>().Where(t => t.Id == req.TourId || t.ParentId == req.TourId);

            var tour = Db.LoadSingleById<Tour>(req.TourId);
            var leader = Db.SingleById<Person>(tour.TourDetail.LeaderId);
            var query = Db.From<Person, PassengerList>()
                .Where<PassengerList>(x => Sql.In(x.TourId, Db.Select(tours).Map(y => y.Id)) && x.HasVisa == req.Have)
                .SelectDistinct(x => x);
            var passengers = Db.Select(query);
            var ticket = new TourPersonReport
            {
                Tour = tour,
                Leader = leader,
                Passengers = passengers,
            };
            return ticket;
        }

        [Authenticate]
        public object Get(GetTourBuyers req)
        {
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound(ErrorCode.TourNotFound.ToString());
            var tour = Db.SingleById<Tour>(req.TourId);
            var blocks = tour.GetAgenciesReport(Db);
            blocks.AddRange(tour.GetTeamReport(Db));
            return blocks;
        }
    }

    public class TourBuyer
    {
        public Guid Id { get; set; }
        public bool Gender { get; set; }
        public bool IsAgency { get; set; } = false;
        public string Title { get; set; }
        public string Prefix { get; set; }
        public string Phone { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }
    }

    public class TourTeammember
    {
        public bool isTeam { get; set; }
        public Guid Id { get; set; }
        public long BasePrice { get; set; }
        public long InfantPrice { get; set; }
        public long FoodPrice { get; set; }
        public long BusPrice { get; set; }
        public long RoomPrice { get; set; }
        public Agency Agency { get; set; }
        public List<Team> Teams { get; set; }

    }
}
