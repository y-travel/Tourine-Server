using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Teams
{
    public class TeamLogic
    {
        public IDbConnection Db { get; }

        public TeamLogic(IDbConnection db)
        {
            Db = db;
        }

        public void Save()
        {
//            var passengerCount = desTour.GetCurrentPassengerCount(Db) + desTour.GetBlocksCapacity(Db);
//            if (passengerCount + req.Passengers.Count > desTour.Capacity)
//                throw HttpError.NotFound("freeSpace");
//
//            //insert new tour
//            var newBlock = new Tour();
//
//            //@TODO limited and unlimited inserted manually, should be read from parent tour
//            var options = Db.Select(Db.From<TourOption>().Where(to => to.TourId == desTour.Id));
//            var tourOptions = new List<TourOption>();
//            var newTeamList = new List<Team>();
//            using (var transDb = Db.OpenTransaction())
//            {
//
//                newBlock.SafePopulate(desTour);
//                newBlock.Status = TourStatus.Creating;
//                newBlock.ParentId = desTour.Id;
//                newBlock.Capacity = req.Passengers.Count;
//                newBlock.AgencyId = req.AgencyId;
//
//                if (req.AgencyId == Session.Agency.Id)
//                    newBlock.Id = desTour.Id;
//                else
//                    Db.Insert(newBlock);
//
//                foreach (OptionType option in Enum.GetValues(typeof(OptionType)))
//                {
//                    if (option == OptionType.Empty) continue;
//                    var newOption = new TourOption
//                    {
//                        TourId = newBlock.Id,
//                        OptionType = option,
//                        Price = options.Find(x => x.OptionType == option).Price,
//                        OptionStatus = option.GetDefaultStatus()
//                    };
//                    tourOptions.Add(newOption);
//                    Db.Insert(newOption);
//                }
//
//                //insert team
//                var teamIds = req.Passengers.Map(x => x.TeamId).Distinct().ToList();
//                var teams = Db.LoadSelect(Db.From<Team>().Where(t => Sql.In(t.Id, teamIds)));
//                foreach (var team in teams)
//                {
//                    var newTeam = new Team();
//                    var sameTeamPassengers = req.Passengers.FindAll(t => t.TeamId == team.Id);
//                    if (!req.Passengers.Exists(x => x.PersonId == team.BuyerId))
//                        newTeam.BuyerIsPassenger = false;
//                    else
//                        Db.UpdateOnly(new Team
//                        {
//                            BuyerIsPassenger = false,
//                        }, onlyFields: t => new
//                        {
//                            t.BuyerIsPassenger
//                        }
//                            , where: x => x.Id == team.Id);
//
//                    newTeam.TourId = newBlock.Id;
//                    newTeam.BuyerId = team.BuyerId;
//                    newTeam.BasePrice = newBlock.BasePrice;
//                    newTeam.Count = sameTeamPassengers.Count;
//                    newTeam.InfantPrice = newBlock.InfantPrice;
//                    newTeam.TotalPrice = teamLogic.CalculateTotalPrice(sameTeamPassengers, tourOptions, newBlock.InfantPrice, newBlock.BasePrice);
//                    newTeam.Buyer = team.Buyer;
//                    Db.Insert(newTeam);
//                    newTeamList.Add(newTeam);
//                    foreach (var passenger in sameTeamPassengers)
//                    {
//                        Db.Delete<PassengerList>(x =>
//                            x.TourId == passenger.TourId &&
//                            x.PersonId == passenger.PersonId &&
//                            x.TeamId == passenger.TeamId);
//                    }
//
//                    //
//                    foreach (var passenger in sameTeamPassengers)
//                    {
//                        if (passenger.Person.IsInfant)
//                        {
//                            Db.Insert(new PassengerList
//                            {
//                                PersonId = passenger.PersonId,
//                                TourId = newBlock.Id,
//                                OptionType = OptionType.Empty,
//                                PassportDelivered = passenger.PassportDelivered,
//                                HaveVisa = passenger.HaveVisa,
//                                TeamId = newTeam.Id,
//                            });
//                        }
//                        else
//                            foreach (var personIncome in passenger.PersonIncomes)
//                            {
//                                Db.Insert(new PassengerList
//                                {
//                                    PersonId = passenger.PersonId,
//                                    TourId = newBlock.Id,
//                                    CurrencyFactor = personIncome.CurrencyFactor,
//                                    IncomeStatus = personIncome.IncomeStatus,
//                                    ReceivedMoney = personIncome.ReceivedMoney,
//                                    OptionType = personIncome.OptionType,
//                                    PassportDelivered = passenger.PassportDelivered,
//                                    HaveVisa = passenger.HaveVisa,
//                                    TeamId = newTeam.Id,
//                                });
//                            }
//                    }
//                }
//                transDb.Commit();
//            }
//            var result = new TourTeammember
//            {
//                isTeam = req.AgencyId == Session.Agency.Id,
//                Id = newBlock.Id,
//                BasePrice = newBlock.BasePrice,
//                InfantPrice = newBlock.InfantPrice,
//                Agency = Db.SingleById<Agency>(req.AgencyId),
//                FoodPrice = options.Find(x => x.OptionType == OptionType.Food).Price,
//                RoomPrice = options.Find(x => x.OptionType == OptionType.Room).Price,
//                BusPrice = options.Find(x => x.OptionType == OptionType.Bus).Price,
//                Teams = newTeamList,
//            };
//            //@TODO ughly
        }

        public void PassengerUniqueCheck(Team team)
        {

        }

        public long CalculateTotalPrice(List<TeamMember> passengers,
            List<TourOption> tourOptions,
            long infantPrice, long basePrice)
        {
            long totalPrice = 0;
            foreach (var passenger in passengers)
            {
                //@TODO: calculate age from birthdate
                if (passenger.Person.IsUnder5)
                {
                    totalPrice += basePrice;
                    totalPrice -= passenger.PersonIncomes.Exists(x => x.OptionType == OptionType.Bus) ? 0 : tourOptions.Find(x => x.OptionType == OptionType.Bus).Price;
                    totalPrice -= passenger.PersonIncomes.Exists(x => x.OptionType == OptionType.Room) ? 0 : tourOptions.Find(x => x.OptionType == OptionType.Room).Price;
                    totalPrice -= passenger.PersonIncomes.Exists(x => x.OptionType == OptionType.Food) ? 0 : tourOptions.Find(x => x.OptionType == OptionType.Food).Price;
                }
                else if (passenger.Person.IsInfant)
                    totalPrice += infantPrice;
                else
                    totalPrice += basePrice;
            }
            return totalPrice;
        }
    }
}
