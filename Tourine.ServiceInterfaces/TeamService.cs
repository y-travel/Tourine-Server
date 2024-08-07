﻿using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public class TeamService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Post(UpsertTeam req)
        {
            if (!Db.Exists<Person>(x => x.Id == req.Buyer.Id))
                throw HttpError.NotFound("buyer not found");
            if (!Db.Exists<Tour>(x => x.Id == req.TourId))
                throw HttpError.NotFound("tour not found");

            var team = new Team();

            if (req.TeamId.HasValue)
            {
                Db.Delete<Passenger>(x => x.TeamId == req.TeamId);
                Db.Delete<Team>(x => x.Id == req.TeamId);
                team.Id = (Guid)req.TeamId;
            }
            var tour = Db.SingleById<Tour>(req.TourId);
            if (tour.FreeSpace < req.Passengers.Count)
                throw HttpError.Forbidden(ErrorCode.NotEnoughFreeSpace.ToString());


            using (IDbTransaction dbTrans = Db.OpenTransaction())
            {
                team.TourId = req.TourId;
                team.BuyerId = req.Buyer.Id;
                team.Count = req.Passengers.Count;
                team.BasePrice = req.BasePrice;
                team.InfantPrice = req.InfantPrice;
                team.TotalPrice = req.TotalPrice;
                team.BuyerIsPassenger = req.Passengers.Exists(x => x.Person.Id == req.Buyer.Id);
                Db.Insert(team);

                List<PassengerInfo> passengers = req.Passengers;
                foreach (var passenger in passengers)
                {
                    if (passenger.Person.IsInfant)
                    {
                        Db.Insert(new Passenger
                        {
                            PersonId = passenger.PersonId,
                            TourId = req.TourId,
                            OptionType = OptionType.Empty,
                            PassportDelivered = passenger.PassportDelivered,
                            HasVisa = passenger.HasVisa,
                            TeamId = team.Id,
                        });
                    }
                    else
                        Db.Insert(new Passenger
                        {
                            PersonId = passenger.PersonId,
                            TourId = req.TourId,
                            OptionType = passenger.OptionType,
                            PassportDelivered = passenger.PassportDelivered,
                            HasVisa = passenger.HasVisa,
                            TeamId = team.Id,
                        });
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
                AutoQuery.CreateQuery(team, Request.GetRequestParams()).Where(t => t.IsPending == false)
            );
        }

        [Authenticate]
        public void Delete(DeleteTeam team)
        {
            if (!Db.Exists<Team>(x => x.Id == team.TeamId))
                throw HttpError.NotFound("");
            Db.Delete<Team>(x => x.Id == team.TeamId);
        }

        [Authenticate]
        public object Get(GetPersonsOfTeam team)
        {
            var q = Db.From<Person, Passenger>((p, pl) => p.Id == pl.PersonId && pl.TeamId == team.TeamId)
                .GroupBy<Person, Passenger>((x, pl) => new
                {
                    x,
                    pl.PassportDelivered,
                    VisaDelivered = pl.HasVisa,
                    pl.OptionType,
                })
                .Select<Person, Passenger>((x, pl) => new
                {
                    x,
                    pl.PassportDelivered,
                    VisaDelivered = pl.HasVisa,
                    SumOptionType = pl.OptionType,
                });

            var items = Db.Select<TempPerson>(q);

            var mainTeam = Db.Single<Team>(x => x.Id == team.TeamId);

            var teams = new List<PassengerInfo>();

            foreach (var item in items)
            {
                var t = new PassengerInfo
                {
                    Person = item.ConvertTo<Person>(),
                    PersonId = item.Id,
                    OptionType = item.SumOptionType,
                    HasVisa = item.VisaDelivered,
                    PassportDelivered = item.PassportDelivered
                };
                teams.Add(t);
            }

            var buyer = Db.SingleById<Person>(mainTeam.BuyerId);
            if (teams.Exists(x => x.Person.Id == buyer.Id))
            {
                var buyerIndex = teams.FindIndex(x => x.Person.Id == buyer.Id);
                teams.Insert(0, teams[buyerIndex]);
                teams.RemoveAt(buyerIndex + 1);
            }

            var teamMember = new TeamPassengers { Buyer = buyer, Passengers = teams };
            return teamMember;
        }

        [Authenticate]
        public void Put(PassengerReplacementTeamAccomplish teamList)
        {
            using (IDbTransaction dbTrans = Db.OpenTransaction())
            {
                foreach (var team in teamList.Teams)
                {
                    Db.UpdateOnly(new Team
                    {
                        InfantPrice = team.InfantPrice,
                        BasePrice = team.BasePrice,
                        IsPending = false,
                    }, onlyFields: t => new
                    {
                        t.InfantPrice,
                        t.BasePrice,
                        IsPanding = t.IsPending,
                    }
                        , @where: p => p.Id == team.Id);
                }
                var replacedPerson = Db.Select(Db.From<Passenger>().Where(x => Sql.In(x.TeamId, teamList.Teams.Map(t => t.Id))).Select(p => new { p.PersonId }));
                Db.Delete<Passenger>(x => Sql.In(x.PersonId, replacedPerson.Map(p => p.PersonId)) && x.TourId == teamList.OldTourId);
                dbTrans.Commit();
            }
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
        public Guid TourId { get; set; }
        public Guid TeamId { get; set; }
    }
}
