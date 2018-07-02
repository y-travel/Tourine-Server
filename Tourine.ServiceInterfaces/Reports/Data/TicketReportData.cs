using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public class TicketReportData
    {
        private IDbConnection Db { get; }
        public int AdultCount { get; set; }

        public int InfantCount { get; set; }

        public List<PassengerInfo> PassengersInfos { get; set; }

        public TourDetail TourDetail { get; set; }
        public TicketReportData(IDbConnection db, Guid? tourId)
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
            InfantCount = PassengersInfos.Count(x => x.Person.IsInfant);
            AdultCount = PassengersInfos.Count(x => !x.Person.IsUnder5 && !x.Person.IsInfant);
        }
    }
}
