using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack;
using Tourine.Models;

namespace Tourine.ServiceInterfaces
{
    public class TourService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetTours query)
        {
            if (string.IsNullOrEmpty(query.Code)) throw HttpError.NotFound("");
            var qry = AutoQuery.CreateQuery(query, Request).And(x => x.Code == query.Code);//@TODO modify, this is mock
            return AutoQuery.Execute(query, qry);
        }
    }
}
