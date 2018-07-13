using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public abstract class PassengerDataReportBase
    {
        protected IDbConnection Db { get; }

        public Guid TourId { get; set; }
        public TourDetail TourDetail { get; set; }
        public List<PassengerInfo> PassengersInfos { get; set; }

        protected PassengerDataReportBase(IDbConnection db)
        {
            Db = db;
        }

        public virtual PassengerDataReportBase FillData(Guid tourId)
        {
            TourId = tourId;
            TourDetail = Db.Select<TourDetail>(Db.From((Tour x, TourDetail y) => x.Id == tourId && y.Id == x.TourDetailId))
                .FirstOrDefault();
            PassengersInfos = Db.LoadSelect(GetPassengerSpecification())
                .ToList();
            PassengersInfos
                .Sort((passengerA, passengerB) => String.Compare(passengerA.Person.Family, passengerB.Person.Family));
            return this;
        }

        public virtual SqlExpression<PassengerInfo> GetPassengerSpecification()
        {
            return Db.From<PassengerInfo>()
                .And(x => Sql.In(x.TourId, TourExtensions.GetChainedTours(Db, TourId)));
        }
    }

    public static class PassengerDataReportBaseExtensions
    {
        public static void LoadLeader(this PassengerDataReportBase passengerDataReport, IDbConnection db)
        {
            var leaderId = passengerDataReport.TourDetail.LeaderId;
            if (leaderId == null)
                return;
            var existLeader = passengerDataReport.PassengersInfos.Find(x => x.PersonId == leaderId);
            if (existLeader != null)
            {
                //if leader is passenger move it to the top of the list
                passengerDataReport.PassengersInfos.Remove(existLeader);
                passengerDataReport.PassengersInfos.Insert(0, existLeader);
            }
            else
            {
                //add leader at the first of the list
                var leader = db.Select<Person>(x => x.Id == leaderId).FirstOrDefault();
                passengerDataReport.PassengersInfos.Insert(0, new PassengerInfo
                {
                    Person = leader,
                    PersonId = leaderId.Value,
                    OptionType = OptionType.All
                });
            }
        }
    }
}
