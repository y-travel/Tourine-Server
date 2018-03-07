using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.AgencyPersons;

namespace Tourine.ServiceInterfaces.Agencies
{
    public class AgencyService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Get(GetAgency agency)
        {
            if (agency.Id == null)
                throw HttpError.NotFound("");

            if (!Db.Exists<Agency>(new { Id = agency.Id }))
                throw HttpError.NotFound("");

            return Db.SingleById<Agency>(agency.Id);
        }

        [Authenticate]
        public object Post(CreateAgency agency)
        {
            Db.Insert(agency.Agency);
            agency.Person.Type = PersonType.Customer;
            Db.Insert(agency.Person);
            Db.Insert(new AgencyPerson {AgencyId = agency.Agency.Id, PersonId = agency.Person.Id});
            return Db.SingleById<Agency>(agency.Agency.Id);
        }

        [Authenticate]
        public void Put(UpdateAgency agency)
        {
            if (!Db.Exists<Agency>(new { Id = agency.Agency.Id }))
                throw HttpError.NotFound("");
            Db.Update(agency.Agency);
        }

        [Authenticate]
        public object Get(GetAgencies agencies)
        {
            var query = AutoQuery.CreateQuery(agencies, Request.GetRequestParams())
                .OrderBy(agency => agency.Name);
            return AutoQuery.Execute(agencies, query);
        }

        [Authenticate]
        public object Get(FindAgency agency)
        {
            if (agency.str == null || agency.str.IsEmpty())
                throw HttpError.NotFound("");
            var item = AutoQuery.CreateQuery(agency, Request.GetRequestParams()).Where(a =>
                a.Name.Contains(agency.str) ||
                a.PhoneNumber.Contains(agency.str));
            return AutoQuery.Execute(agency, item);
        }
    }
}
