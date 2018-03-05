using System;
using ServiceStack;
using ServiceStack.FluentValidation.Attributes;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Persons
{
    public class PersonService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Post(CreatePerson createPerson)
        {
            Db.Insert(createPerson.Person);
            return Db.SingleById<Person>(createPerson.Person.Id);
        }

        [Authenticate]
        public void Put(UpdatePerson updatePerson)
        {
            if (!Db.Exists<Person>(new { Id = updatePerson.Person.Id }))
                throw HttpError.NotFound("");
            Db.Update(updatePerson.Person);
        }

        [Authenticate]
        public object Get(GetPersons getPerson)
        {
            var query = getPerson.Id.HasValue
                ? AutoQuery.CreateQuery(getPerson, Request.GetRequestParams()).Where(x => x.Id == getPerson.Id)
                : AutoQuery.CreateQuery(getPerson, Request.GetRequestParams());
            return AutoQuery.Execute(getPerson, query);
        }

        [Authenticate]
        public void Delete(DeletePerson deletePerson)
        {
            if (deletePerson.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Person>(new { Id = deletePerson.Id }))
                throw HttpError.NotFound("");
            Db.DeleteById<Person>(deletePerson.Id);
        }

        [Authenticate]
        public object Get([Validator(typeof(FindPersonFromNc))]FindPersonFromNc fromNc)
        {
            if (!Db.Exists<Person>(new { NationalCode = fromNc.NationalCode }))
                throw HttpError.NotFound("");
            return Db.Single<Person>(new { NationalCode = fromNc.NationalCode });
        }

        [Authenticate]
        public object Get(FindPersonInAgency persons)
        {
            if (!Db.Exists<Agency>(new { Id = persons.AgencyId }))
                throw HttpError.NotFound("");
            var item = AutoQuery.CreateQuery(persons, Request.GetRequestParams()).Where(p =>
                p.Name.Contains(persons.Str) ||
                p.Family.Contains(persons.Str) ||
                p.MobileNumber.Contains(persons.Str));
            return AutoQuery.Execute(persons, item);
        }

        //[Authenticate]
        public object Get(GetLeaders leaders)
        {
            var query = AutoQuery.CreateQuery(leaders, Request.GetRequestParams())
                .Where($"({nameof(Person.Type)} & {(int)PersonType.Leader} = {(int)PersonType.Leader})");
            return AutoQuery.Execute(leaders, query);
        }

        [Authenticate]
        public object Post(RegisterPerson regCmd)
        {
            if (!Db.Exists<Tour>(new { Id = regCmd.TourId }))
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
                Db.Insert(new TeamPerson
                {
                    TeamId = teamId,
                    PersonId = passenger
                });
            }
            return Db.SingleById<Team>(teamId);
        }

        [Authenticate]
        public object Get(GetCurrentPerson person)
        {
            return Db.SingleById<Person>(Session.User.PersonId);
        }
    }
}
