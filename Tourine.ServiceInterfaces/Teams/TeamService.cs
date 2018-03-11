using System;
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
        [Authenticate]
        public void Post(CreateTeam req)
        {
            if (!Db.Exists<Person>(x => x.Id == req.Buyer.PersonId))
                throw HttpError.NotFound("");
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound("");

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
                        });
                    }
                    Db.Insert(new TeamPerson
                    {
                        PersonId = passenger.PersonId,
                        TeamId = team.Id
                    });
                }
                dbTrans.Commit();
            }


        }

        [Authenticate]
        public void Put(UpdateTeam team)
        {
            if (!Db.Exists<Team>(new { Id = team.Team.Id }))
                throw HttpError.NotFound("");
            Db.Update(team.Team);//@TODO: dont update submitDate
        }
    }
}
