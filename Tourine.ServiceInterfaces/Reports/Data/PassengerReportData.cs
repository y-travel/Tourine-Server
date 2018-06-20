using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public class PassengerReportData
    {
        public IDbConnection Db { get; }
        public Guid TourId { get; }
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
            TourId = tourId.Value;
            FillData(TourId);
        }

        public void FillData(Guid tourId)
        {
            PassengersInfos = Db.LoadSelect<PassengerInfo>().Where(x => x.TourId == TourId).ToList();
            PassengerCount = PassengersInfos.Count;
            InfantCount = PassengersInfos.Count(x => x.Person.IsInfant);
            AdultCount = PassengersInfos.Count(x => !x.Person.IsUnder5 && !x.Person.IsInfant);
            BedCount = PassengersInfos.Count(x => x.OptionType.HasFlag(OptionType.Room));
            FoodCount = PassengersInfos.Count(x => x.OptionType.HasFlag(OptionType.Food));
        }
    }
}
