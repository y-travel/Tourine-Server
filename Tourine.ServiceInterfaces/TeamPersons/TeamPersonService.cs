using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.TeamPassengers
{
    public class TeamPersonService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public void Post(AddPersonToTeam personToTeam)
        {
            if (!Db.Exists<Person>(new { Id = personToTeam.TeamPerson.PersonId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Team>(new { Id = personToTeam.TeamPerson.TeamId }))
                throw HttpError.NotFound("");
            Db.Insert(personToTeam.TeamPerson);
        }

        [Authenticate]
        public void Put(ChangePersonsTeam personsTeam)
        {
            if (!Db.Exists<Person>(new { Id = personsTeam.TeamPerson.PersonId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Team>(new { Id = personsTeam.TeamPerson.TeamId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<TeamPerson>(new { Id = personsTeam.TeamPerson.Id }))
                throw HttpError.NotFound("");
            Db.Update(personsTeam.TeamPerson);
        }

        [Authenticate]
        public object Get(GetPersonsOfTeam team)
        {
            var passengers = AutoQuery.CreateQuery(team, Request.GetRequestParams());
            return AutoQuery.Execute(team, passengers);
        }
    }
}
