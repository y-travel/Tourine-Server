using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public abstract class ReportBase
    {
        protected IDbConnection Db { get; }

        public Guid TourId { get; set; }
        public TourDetail TourDetail { get; set; }
        public List<PassengerInfo> PassengersInfos { get; set; }
        public int PassengerCount { get; set; }


        protected ReportBase(IDbConnection db)
        {
            Db = db;
        }

        public virtual ReportBase FillData(Guid tourId)
        {
            TourId = tourId;
            TourDetail = Db.Select<TourDetail>(Db.From((Tour x, TourDetail y) => x.Id == tourId && y.Id == x.TourDetailId))
                .FirstOrDefault();
            PassengersInfos = Db.LoadSelect<PassengerInfo>()
                .Where(x => Sql.In(x.TourId, TourExtensions.GetChainedTours(Db, tourId)))
                .ToList();
            PassengerCount = PassengersInfos.Count;
            return this;
        }
    }
}
