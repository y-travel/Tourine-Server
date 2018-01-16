using ServiceStack;

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
    }
}
