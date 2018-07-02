using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public class PassengerReportData
    {
        private IDbConnection Db { get; }
        public int PassengerCount { get; set; }

        public int AdultCount { get; set; }

        public int InfantCount { get; set; }

        public int BedCount { get; set; }

        public int FoodCount { get; set; }
        public List<PassengerInfo> PassengersInfos { get; set; }
        public PassengerReportData(IDbConnection db, Guid? tourId)
        {
            Db = db;
            if (!tourId.HasValue)
                return;
            FillData(tourId.Value);
        }

        public void FillData(Guid tourId)
        {
            PassengersInfos = Db.LoadSelect<PassengerInfo>()
                .Where(x => Sql.In(x.TourId, TourExtensions.GetChainedTours(Db, tourId)))
                .ToList();
            PassengerCount = PassengersInfos.Count;
            InfantCount = PassengersInfos.Count(x => x.Person.IsInfant);
            AdultCount = PassengersInfos.Count(x => !x.Person.IsUnder5 && !x.Person.IsInfant);
            BedCount = PassengersInfos.Count(x => x.OptionType.HasFlag(OptionType.Room));
            FoodCount = PassengersInfos.Count(x => x.OptionType.HasFlag(OptionType.Food));
        }
    }
}
