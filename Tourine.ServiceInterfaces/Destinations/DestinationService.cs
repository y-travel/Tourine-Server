using System.Net;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Destinations
{
    public class DestinationService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetDestinations destReq)
        {
            var query = AutoQuery.CreateQuery(destReq, Request.GetRequestParams());
            return AutoQuery.Execute(destReq, query);
        }

        public void Post(PostDestination destination)
        {
            Db.Insert(destination.Destination);
        }

        public void Put(PutDestination destination)
        {
            if (!Db.Exists<Destination>(new { Id = destination.Destination.Id }))
                throw HttpError.NotFound(""); 
            Db.Update(destination.Destination);
        }
    }
}
