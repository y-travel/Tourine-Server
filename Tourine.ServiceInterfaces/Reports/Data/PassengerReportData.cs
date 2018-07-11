using System;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public class PassengerReportData : ReportBase
    {
        public int AdultCount { get; set; }
        public int InfantCount { get; set; }
        public int BedCount { get; set; }
        public int FoodCount { get; set; }
        public Person Leader { get; set; }

        public PassengerReportData(IDbConnection db) : base(db)
        {
        }
        public override ReportBase FillData(Guid tourId)
        {
            base.FillData(tourId);
            LoadLeader();
            InfantCount = PassengersInfos.Count(x => x.Person.IsInfant);
            AdultCount = PassengersInfos.Count(x => !x.Person.IsUnder5 && !x.Person.IsInfant);
            BedCount = PassengersInfos.Count(x => x.OptionType.HasFlag(OptionType.Room));
            FoodCount = PassengersInfos.Count(x => x.OptionType.HasFlag(OptionType.Food));
            return this;
        }
        public void LoadLeader(){
            if (TourDetail.LeaderId == null)
                return;
            Leader = Db.Select<Person>(x => x.Id == TourDetail.LeaderId).FirstOrDefault();
            PassengersInfos.Add(new PassengerInfo { Person = Leader, PersonId = TourDetail.LeaderId.Value, OptionType = OptionType.All });
        }
    }
}
