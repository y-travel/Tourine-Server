﻿using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Persons
{
    public class PersonService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Post(AddNewPerson addNewPerson)
        {
            Db.Insert(addNewPerson.Person);
            return Db.SingleById<Person>(addNewPerson.Person.Id);
        }

        [Authenticate]
        public object Put(UpdatePerson updatePerson)
        {
            if (!Db.Exists<Person>(new { Id = updatePerson.Person.Id }))
                throw HttpError.NotFound("");
            Db.Update(updatePerson.Person);
            return Db.SingleById<Person>(updatePerson.Person.Id);
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
        public object Get(FindPersonFromNc fromNc)
        {
            if (!Db.Exists<Person>(new { NationalCode = fromNc.NationalCode }))
                throw HttpError.NotFound("");
            return Db.Single<Person>(new { NationalCode = fromNc.NationalCode });
        }

        [Authenticate]
        public object Get(FindPersonInAgency persons)
        {
            if (!persons.AgencyId.HasValue)
                persons.AgencyId = Session.Agency.Id;

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
        public object Get(GetCurrentPerson person)
        {
            return Db.SingleById<Person>(Session.User.PersonId);
        }

        [Authenticate]
        public object Post(UpsertLeader leader)
        {
            leader.Person.Type = leader.Person.Type | PersonType.Leader;
            Db.Save(leader.Person);

            return Db.SingleById<Person>(leader.Person.Id);
        }

        [Authenticate]
        public void Delete(DeleteLeader req)
        {
            if (req.Id == null)
                throw HttpError.NotFound("");
            if (!Db.Exists<Person>(x=> x.Id == req.Id))
                throw HttpError.NotFound("");
           var leader = Db.SingleById<Person>(req.Id);
            leader.Type = leader.Type & (~PersonType.Leader);
            Db.Update(leader);
        }
    }

}
