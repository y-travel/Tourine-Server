using System.Collections.Generic;
using System.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Services;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Teams
{
    public class TeamService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public void Post(UpsertTeam req)
        {
            if (!Db.Exists<Person>(x => x.Id == req.Buyer.PersonId))
                throw HttpError.NotFound("");
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound("");

            if (req.TeamId.HasValue)
            {
                Db.Delete<Team>(x => x.Id == req.TourId);
                Db.Delete<PassengerList>(x => x.TeamId == req.TeamId);
            }
            var tour = Db.SingleById<Tour>(req.TourId);
            if (tour.Capacity <= req.Passengers.Count)
                throw HttpError.NotFound("freeSpace");

            using (IDbTransaction dbTrans = Db.OpenTransaction())
            {
                var team = new Team();
                team.TourId = req.TourId;
                team.BuyerId = req.Buyer.PersonId;
                team.Count = req.Passengers.Count + 1;
                Db.Insert(team);

                List<TeamMember> passengers = req.Passengers;
                passengers.Insert(0, req.Buyer);
                foreach (var passenger in passengers)
                {
                    foreach (var personIncome in passenger.PersonIncomes)
                    {
                        Db.Insert(new PassengerList
                        {
                            PersonId = passenger.PersonId,
                            TourId = req.TourId,
                            CurrencyFactor = personIncome.CurrencyFactor,
                            IncomeStatus = personIncome.IncomeStatus,
                            ReceivedMoney = personIncome.ReceivedMoney,
                            OptionType = personIncome.OptionType,
                            PassportDelivered = passenger.PassportDelivered,
                            VisaDelivered = passenger.VisaDelivered,
                            TeamId = team.Id,
                        });
                    }
                }
                dbTrans.Commit();
            }


        }

        [Authenticate]
        public object Get(GetTourTeams team)
        {
            if (!Db.Exists<Team>(x=> x.TourId == team.TourId))
                throw HttpError.NotFound("");
            return AutoQuery.Execute(
                team,
                AutoQuery.CreateQuery(team, Request.GetRequestParams())
            );
        }

        [Authenticate]
        public void Delete(DeleteTeam team)
        {
            if (!Db.Exists<Team>(x => x.Id == team.TeamId))
                throw HttpError.NotFound("");
            Db.Delete<TeamPerson>(x => x.TeamId == team.TeamId);
            Db.Delete<Team>(x => x.Id == team.TeamId);
        }
    }
}
