using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using Tourine.Models;

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
