using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public class TeamLogic
    {
        public IDbConnection Db { get; }

        public TeamLogic(IDbConnection db)
        {
            Db = db;
        }

        public Team CopyPassengers(Team fromTeam, Tour toTour, List<TeamMember> passengers)
        {
            //@TODO: check existance with list (not per one query)
            if (passengers.TrueForAll(x => toTour.IsPassengerExist(x.Person.Id, Db)))
                throw HttpError.Forbidden("Passenger exist in destination tour");

            var newTeam = new Team();
            if (!passengers.Exists(x => x.PersonId == fromTeam.BuyerId))
                newTeam.BuyerIsPassenger = false;
            else
                Db.UpdateOnly(new Team { BuyerIsPassenger = false, }
                , onlyFields: t => new { t.BuyerIsPassenger }
                    , @where: x => x.Id == fromTeam.Id);

            newTeam.TourId = toTour.Id;
            newTeam.BuyerId = fromTeam.BuyerId;
            newTeam.BasePrice = toTour.BasePrice;
            newTeam.Count = passengers.Count;
            newTeam.InfantPrice = toTour.InfantPrice;
            newTeam.TotalPrice = CalculateTotalPrice(passengers, toTour);
            newTeam.Buyer = fromTeam.Buyer;
            newTeam.SubmitDate = DateTime.Now;
            newTeam.IsPending = true;

            Db.Insert(newTeam);
            foreach (var passenger in passengers)
            {
                if (passenger.Person.IsInfant)
                    Db.Insert(new PassengerList
                    {
                        PersonId = passenger.PersonId,
                        TourId = toTour.Id,
                        OptionType = OptionType.Empty,
                        PassportDelivered = passenger.PassportDelivered,
                        HasVisa = passenger.HasVisa,
                        TeamId = newTeam.Id,
                    });
                else
                    Db.Insert(new PassengerList
                    {
                        PersonId = passenger.PersonId,
                        TourId = toTour.Id,
                        OptionType = passenger.OptionType,
                        PassportDelivered = passenger.PassportDelivered,
                        HasVisa = passenger.HasVisa,
                        TeamId = newTeam.Id,
                    });
            }
            return newTeam;
        }

        public void PassengerUniqueCheck(Team team)
        {

        }

        public long CalculateTotalPrice(List<TeamMember> passengers,
            Tour tour)
        {
            var tourOptions = Db.Select(Db.From<TourOption>().Where(to => to.TourId == tour.Id));

            long totalPrice = 0;
            foreach (var passenger in passengers)
            {
                //@TODO: calculate age from birthdate
                if (passenger.Person.IsUnder5)
                {
                    totalPrice += tour.BasePrice;
                    foreach (int value in Enum.GetValues(typeof(OptionType)))
                        totalPrice -= ((int)passenger.OptionType & value) == 0
                             ? tourOptions.Find(x => x.OptionType == OptionType.Bus).Price
                             : 0;
                }
                else if (passenger.Person.IsInfant)
                    totalPrice += tour.InfantPrice;
                else
                    totalPrice += tour.BasePrice;
            }
            return totalPrice;
        }
    }
}
