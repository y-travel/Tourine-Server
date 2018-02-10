using System;
using System.Net;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Destinations
{
    public class DestinationService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Get(GetDestinations destReq)
        {
            var query = AutoQuery.CreateQuery(destReq, Request.GetRequestParams());
            return AutoQuery.Execute(destReq, query);
        }

        [Authenticate]
        public object Post(CreateDestination destination)
        {
            destination.Destination.Id = Guid.NewGuid();
            Db.Insert(destination.Destination);
            return Db.SingleById<Destination>(destination.Destination.Id);
        }

        [Authenticate]
        public void Put(UpdateDestination destination)
        {
            if (!Db.Exists<Destination>(new { Id = destination.Destination.Id }))
                throw HttpError.NotFound(""); 
            Db.Update(destination.Destination);
        }
    }
}
