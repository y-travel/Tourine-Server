using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public class VisaReportData : PassengerDataReportBase
    {
        public Dictionary<Guid, string> BuyerNames { get; set; }
        public VisaReportData(IDbConnection db) : base(db)
        {
        }

        public override PassengerDataReportBase FillData(Guid tourId)
        {
            base.FillData(tourId);
            BuyerNames = new Dictionary<Guid, string>();
            PassengersInfos.ForEach(x => BuyerNames[x.PersonId] = x.GetBuyerName(tourId, Db));
            return this;
        }

        public override SqlExpression<PassengerInfo> GetPassengerSpecification()
        {
            return base.GetPassengerSpecification().And(x => !x.HasVisa);
        }
    }
}
