using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class PassengerService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }
        [Authenticate]
        public void Post(CreatePassenger createPassenger)
        {
            Db.Insert(createPassenger.Passenger);
        }

        [Authenticate]
        public void Put(UpdatePassenger updatePassenger)
        {
            if (!Db.Exists<Passenger>(new { Id = updatePassenger.Passenger.Id }))
                throw HttpError.NotFound("");
            Db.Update(updatePassenger.Passenger);
        }

        [Authenticate]
        public object Get(GetPassengers getPassenger)
        {
            var query = AutoQuery.CreateQuery(getPassenger, Request.GetRequestParams());
            return AutoQuery.Execute(getPassenger, query);
        }

        [Authenticate]
        public void Delete(DeletePassenger deletePassenger)
        {
            if (deletePassenger.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Passenger>(new { Id = deletePassenger.Id }))
                throw HttpError.NotFound("");
            Db.DeleteById<Passenger>(deletePassenger.Id);
        }

        [Authenticate]
        public object Get(FindPassengerFromNc fromNc)
        {
            if (!Db.Exists<Passenger>(new { NationalCode = fromNc.NationalCode }))
                throw HttpError.NotFound("");
            return Db.Single<Passenger>(new { NationalCode = fromNc.NationalCode });
        }

        [Authenticate]
        public object Get(FindPassengerInAgency passengers)
        {
            if (!Db.Exists<Agency>(new { Id = passengers.AgencyId }))
                throw HttpError.NotFound("");
            var item = AutoQuery.CreateQuery(passengers, Request.GetRequestParams()).Where(p =>
                p.Name.Contains(passengers.Str) ||
                p.Family.Contains(passengers.Str) ||
                p.MobileNumber.Contains(passengers.Str));
            var it = AutoQuery.Execute(passengers, item);
            return it;
        }

        [Authenticate]
        public object Get(GetLeaders leaders)
        {
            var query = AutoQuery.CreateQuery(leaders, Request.GetRequestParams())
                .Where(passenger => passenger.Type == PassengerType.Leader);
            return AutoQuery.Execute(leaders, query);
        }

        [Authenticate]
        public object Post(RegisterPassenger regCmd)
        {
            if (!Db.Exists<Tour>(new { Id = regCmd.TourId}))
                throw HttpError.NotFound("");
       
            var teamId = Guid.NewGuid();
            Db.Insert(new Team
            {
                Id = teamId,
                BuyerId = regCmd.BuyerId,
                TourId = regCmd.TourId,
                Count = regCmd.PassengersId.Count + 1,
                SubmitDate = DateTime.Now
            });
            foreach (var passenger in regCmd.PassengersId)
            {
                Db.Insert(new TeamPassenger
                {
                    TeamId = teamId,
                    PassengerId = passenger
                });
            }
            return Db.SingleById<Team>(teamId);
        }
    }
}
