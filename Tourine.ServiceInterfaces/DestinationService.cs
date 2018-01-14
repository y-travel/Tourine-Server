using ServiceStack;
using Tourine.ServiceInterfaces.Destinations;

namespace Tourine.ServiceInterfaces
{
    public class DestinationService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetDestinations destReq)
        {
            var query = AutoQuery.CreateQuery(destReq, Request.GetRequestParams());
            return AutoQuery.Execute(destReq, query);
        }
    }
}
