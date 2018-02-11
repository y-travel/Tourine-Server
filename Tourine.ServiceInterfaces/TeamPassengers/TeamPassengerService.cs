using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class TeamPassengerService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public void Post(AddPassengerToTeam passengerToTeam)
        {
            if (!Db.Exists<Passenger>(new { Id = passengerToTeam.TeamPassenger.PassengerId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Team>(new { Id = passengerToTeam.TeamPassenger.TeamId }))
                throw HttpError.NotFound("");
            Db.Insert(passengerToTeam.TeamPassenger);
        }

        [Authenticate]
        public void Put(ChangePassengerTeam passengerTeam)
        {
            if (!Db.Exists<Passenger>(new { Id = passengerTeam.TeamPassenger.PassengerId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Team>(new { Id = passengerTeam.TeamPassenger.TeamId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<TeamPassenger>(new { Id = passengerTeam.TeamPassenger.Id }))
                throw HttpError.NotFound("");
            Db.Update(passengerTeam.TeamPassenger);
        }

        [Authenticate]
        public object Get(GetPassengerOfTeam team)
        {
            var passengers = AutoQuery.CreateQuery(team, Request.GetRequestParams());
            return AutoQuery.Execute(team, passengers);
        }
    }
}
