using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public class AgencyPersonService : AppService
    {
        public IAutoQueryDb AutoQueryDb { get; set; }

        [Authenticate]
        public object Get(GetPersonOfAgency agency)
        {
            if (agency.AgencyId == null || agency.AgencyId == Guid.Empty)
                agency.AgencyId = Session.Agency.Id;
            if (!Db.Exists<Agency>(new { Id = agency.AgencyId }))
                throw HttpError.NotFound("");
            var items = AutoQueryDb.CreateQuery(agency, Request.GetRequestParams());
            return AutoQueryDb.Execute(agency, items);
        }

        [Authenticate]
        public object Post(AddPersonToAgency toAgency)
        {
            if (toAgency.AgencyId == null || toAgency.AgencyId == Guid.Empty)
                toAgency.AgencyId = Session.Agency.Id;
            if (!Db.Exists<Agency>(new { Id = toAgency.AgencyId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Person>(new { Id = toAgency.PersonId }))
                throw HttpError.NotFound("");
            Db.Insert(new AgencyPerson
            {
                AgencyId = toAgency.AgencyId.Value,
                PersonId = toAgency.PersonId
            });
            return Db.Single<AgencyPerson>(x =>
                x.AgencyId == toAgency.AgencyId.Value && x.PersonId == toAgency.PersonId);
        }

        [Authenticate]
        public void Put(UpdatePersonToAgency toAgency)
        {
            if (!Db.Exists<Agency>(new { Id = toAgency.AgencyId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<Person>(new { Id = toAgency.PersonId }))
                throw HttpError.NotFound("");
            if (!Db.Exists<AgencyPerson>(new { Id = toAgency.Id }))
                throw HttpError.NotFound("");
            Db.Update(new AgencyPerson
            {
                Id = toAgency.Id,
                AgencyId = toAgency.AgencyId,
                PersonId = toAgency.PersonId
            });
        }
    }
}
