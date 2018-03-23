﻿using System;
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
        public object Post(UpsertTeam req)
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

            var team = new Team();

            using (IDbTransaction dbTrans = Db.OpenTransaction())
            {
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
            return Db.SingleById<Team>(team.Id);
        }

        [Authenticate]
        public object Get(GetTourTeams team)
        {
            if (!Db.Exists<Team>(x => x.TourId == team.TourId))
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

        [Authenticate]
        public object Get(GetPersonsOfTeam team)
        {
            var q = Db.From<Person, PassengerList>((p, pl) => p.Id == pl.PersonId && pl.TeamId == team.TeamId)
                .GroupBy<Person, PassengerList>((x, pl) => new
                {
                    x,
                    pl.PassportDelivered,
                    pl.VisaDelivered
                })
                .Select<Person, PassengerList>((x, pl) => new
                {
                    x,
                    pl.PassportDelivered,
                    pl.VisaDelivered,
                    SumOptionType = Sql.Sum(nameof(PassengerList) + "." + nameof(PassengerList.OptionType)),
                });

            var items = Db.Select<TempPerson>(q);
            var teams = new List<TeamMember>();

            foreach (var item in items)
            {
                var t = new TeamMember
                {
                    Person = item.ConvertTo<Person>(),
                    PersonId = item.Id,
                    PersonIncomes = item.SumOptionType.GetListOfTypes(),
                    VisaDelivered = item.VisaDelivered,
                    PassportDelivered = item.PassportDelivered
                };
                teams.Add(t);
            }
            return teams;
        }
    }

    public class TempPerson
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Family { get; set; }
        public string EnglishName { get; set; }
        public string EnglishFamily { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? PassportExpireDate { get; set; }
        public DateTime? VisaExpireDate { get; set; }
        public string PassportNo { get; set; }
        public bool Gender { get; set; }
        public PersonType Type { get; set; } = PersonType.Passenger;

        public bool IsUnder5 { get; set; }
        public bool IsInfant { get; set; }

        public OptionType SumOptionType { get; set; }
        public bool PassportDelivered { get; set; }
        public bool VisaDelivered { get; set; }
    }
}
