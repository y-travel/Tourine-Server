using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Web;
using Tourine.Models;

namespace Tourine.ServiceInterfaces
{
    public class TourService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }
        public object Get(GetTour reqTour)
        {
            if (reqTour.Id == null)
                throw HttpError.NotFound("");

            var tour = Db.SingleById<Tour>(reqTour.Id);
            Db.LoadReferences(tour);
            return tour;
        }

        public object Get(GetTours reqTours)
        {
            var tours = AutoQuery.CreateQuery(reqTours, Request.GetRequestParams());
            return AutoQuery.Execute(reqTours, tours);
        }

        public object Post(PostTour postReq)
        {
            postReq.Tour.Id = Guid.NewGuid();
            var id = Db.Insert(postReq.Tour);
            var insertedItem = Db.SingleById<Tour>(postReq.Tour.Id);
            return insertedItem;
        }
    }
}


