using ServiceStack;
using ServiceStack.OrmLite;

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

            if (!Db.Exists<Agency>(new  {Id = agency.Id}))
                throw HttpError.NotFound("");

            return Db.SingleById<Agency>(agency.Id);
        }

        [Authenticate]
        public void Post(CreateAgency agency )
        {
            Db.Insert(agency.Agency);
        }
        
        [Authenticate]
        public void Put(UpdateAgency agency)
        {
            if (!Db.Exists<Agency>(new {Id = agency.Agency.Id}))
                throw HttpError.NotFound("");
            Db.Update(agency.Agency);
        }

        [Authenticate]
        public object Get(GetAgencies agencies)
        {
            var query = AutoQuery.CreateQuery(agencies, Request.GetRequestParams());
            return AutoQuery.Execute(agencies, query);
        }
    }
}
