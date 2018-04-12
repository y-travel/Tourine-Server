using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Passengers
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

            //calculate destination tour free space
            var passengerCount = desTour.GetCurrentPassengerCount(Db) + desTour.GetBlocksCapacity(Db);
            if (passengerCount + req.Passengers.Count > desTour.Capacity)
                throw HttpError.NotFound("freeSpace");

            //insert new tour
            var newBlock = new Tour();

            //@TODO limited and unlimited inserted manually, should be read from parent tour
            var options = Db.Select(Db.From<TourOption>().Where(to => to.TourId == desTour.Id));
            var tourOptions = new List<TourOption>();
            var newTeamList = new List<Team>();
            using (var transDb = Db.OpenTransaction())
            {

                newBlock.SafePopulate(desTour);
                newBlock.Status = TourStatus.Creating;
                newBlock.ParentId = desTour.Id;
                newBlock.Capacity = req.Passengers.Count;
                newBlock.AgencyId = req.AgencyId;
                if (req.AgencyId == Session.Agency.Id)
                    newBlock.Id = desTour.Id;
                else
                    Db.Insert(newBlock);
                foreach (OptionType option in Enum.GetValues(typeof(OptionType)))
                {
                    if (option == OptionType.Empty) continue;
                    var newOption = new TourOption
                    {
                        TourId = newBlock.Id,
                        OptionType = option,
                        Price = options.Find(x => x.OptionType == option).Price,
                        OptionStatus = option.GetDefaultStatus()
                    };
                    tourOptions.Add(newOption);
                    Db.Insert(newOption);
                }

                //insert team
                var teamLogic = new TeamLogics();
                var teamIds = req.Passengers.Map(x => x.TeamId).Distinct().ToList();
                var teams = Db.Select(Db.From<Team>().Where(t => Sql.In(t.Id, teamIds)));
                foreach (var team in teams)
                {
                    var newTeam = new Team();
                    var sameTeamPassengers = req.Passengers.FindAll(t => t.TeamId == team.Id);
                    if (!req.Passengers.Exists(x => x.PersonId == team.BuyerId))
                        newTeam.BuyerIsPassenger = false;
                    else
                        Db.UpdateOnly(new Team
                        {
                            BuyerIsPassenger = false,
                        }, onlyFields: t => new
                        {
                            t.BuyerIsPassenger
                        }
                            , where: x => x.Id == team.Id);

                    newTeam.TourId = newBlock.Id;
                    newTeam.BuyerId = team.BuyerId;
                    newTeam.BasePrice = newBlock.BasePrice;
                    newTeam.Count = sameTeamPassengers.Count;
                    newTeam.InfantPrice = newBlock.InfantPrice;
                    newTeam.TotalPrice = teamLogic.CalculateTotalPrice(sameTeamPassengers, tourOptions, newBlock.InfantPrice, newBlock.BasePrice);
                    Db.Insert(newTeam);
                    newTeamList.Add(newTeam);
                    foreach (var passenger in sameTeamPassengers)
                    {
                        Db.Delete<PassengerList>(x =>
                            x.TourId == passenger.TourId &&
                            x.PersonId == passenger.PersonId &&
                            x.TeamId == passenger.TeamId);
                    }

                    //
                    foreach (var passenger in sameTeamPassengers)
                    {
                        if (passenger.Person.IsInfant)
                        {
                            Db.Insert(new PassengerList
                            {
                                PersonId = passenger.PersonId,
                                TourId = newBlock.Id,
                                OptionType = OptionType.Empty,
                                PassportDelivered = passenger.PassportDelivered,
                                HaveVisa = passenger.HaveVisa,
                                TeamId = newTeam.Id,
                            });
                        }
                        else
                            foreach (var personIncome in passenger.PersonIncomes)
                            {
                                Db.Insert(new PassengerList
                                {
                                    PersonId = passenger.PersonId,
                                    TourId = newBlock.Id,
                                    CurrencyFactor = personIncome.CurrencyFactor,
                                    IncomeStatus = personIncome.IncomeStatus,
                                    ReceivedMoney = personIncome.ReceivedMoney,
                                    OptionType = personIncome.OptionType,
                                    PassportDelivered = passenger.PassportDelivered,
                                    HaveVisa = passenger.HaveVisa,
                                    TeamId = newTeam.Id,
                                });
                            }
                    }
                }
                transDb.Commit();
            }
            var result = new TourTeammember
            {
                TourId = newBlock.Id,
                BasePrice = newBlock.BasePrice,
                InfantPrice = newBlock.InfantPrice,
                FoodPrice = options.Find(x => x.OptionType == OptionType.Food).Price,
                RoomPrice = options.Find(x => x.OptionType == OptionType.Room).Price,
                BusPrice = options.Find(x => x.OptionType == OptionType.Bus).Price,
                Teams = newTeamList,
            };
            //@TODO ughly
            return result;
        }
    }

    public class TourTeammember
    {
        public Guid TourId { get; set; }
        public long BasePrice { get; set; }
        public long InfantPrice { get; set; }
        public long FoodPrice { get; set; }
        public long BusPrice { get; set; }
        public long RoomPrice { get; set; }
        public List<Team> Teams { get; set; }

    }
}
