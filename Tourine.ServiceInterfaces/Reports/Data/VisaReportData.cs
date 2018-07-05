using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public class VisaReportData
    {
        private IDbConnection Db { get; }
        public int PassengerCount { get; set; }
        public List<PassengerInfo> PassengersInfos { get; set; }
        public TourDetail TourDetail { get; set; }
        public Dictionary<Guid, string> BuyerNames { get; set; }
        public VisaReportData(IDbConnection db, Guid? tourId)
        {
            Db = db;
            if (!tourId.HasValue)
                return;
            FillData(tourId.Value);
        }

        public void FillData(Guid tourId)
        {
            TourDetail = Db.Select<TourDetail>(Db.From((Tour x, TourDetail y) => x.Id == tourId && y.Id == x.TourDetailId))
                .FirstOrDefault();
            PassengersInfos = Db.LoadSelect<PassengerInfo>()
                .Where(x => Sql.In(x.TourId, TourExtensions.GetChainedTours(Db, tourId)))
                .ToList();
            PassengerCount = PassengersInfos.Count;
            BuyerNames = new Dictionary<Guid, string>();
            PassengersInfos.ForEach(x => BuyerNames[x.PersonId] = x.GetBuyerName(tourId, Db));
        }
    }
}
