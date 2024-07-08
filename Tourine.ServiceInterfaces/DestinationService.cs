using System;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public class DestinationService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        [Authenticate]
        public object Get(GetDestinations destReq)
        {
            var query = destReq.Id.HasValue
                ? AutoQuery.CreateQuery(destReq, Request.GetRequestParams()).Where(x => x.Id == destReq.Id)
                : AutoQuery.CreateQuery(destReq, Request.GetRequestParams());
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
