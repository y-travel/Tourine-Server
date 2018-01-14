using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using Tourine.ServiceInterfaces.Places;

namespace Tourine.ServiceInterfaces
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
