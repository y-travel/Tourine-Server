using ServiceStack;

namespace Tourine.ServiceInterfaces.Places
{
    public class PlaceService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetPlace getPlace)
        {
            var query = AutoQuery.CreateQuery(getPlace, Request.GetRequestParams());
            return AutoQuery.Execute(getPlace, query);
        }
    }
}
