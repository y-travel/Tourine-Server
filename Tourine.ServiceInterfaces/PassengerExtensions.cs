using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public static class PassengerExtensions
    {
        public static string GetBuyerName(this PassengerInfo passenger, Guid currentTourId, IDbConnection db) =>
            passenger.TourId == currentTourId
            ? passenger.Person.DisplayTitle
            : passenger.TourId.GetAgency(db)?.DisplayTitle;

        public static TourPassengers GetPassengers(IDbConnection Db, Guid tourId, bool? hasVisa)
        {
            var tour = Db.LoadSingleById<Tour>(tourId);
            if (tour == null)
                throw HttpError.NotFound(ErrorCode.TourNotFound.ToString());
            var tours = Db.From<Tour>().Where(t => t.Id == tourId || t.ParentId == tourId);

            var leader = Db.SingleById<Person>(tour.TourDetail.LeaderId);
            var query = Db.From<PassengerInfo, Person>()
                .Where<Passenger>(x => Sql.In(x.TourId, Db.Select(tours).Map(y => y.Id)) && x.HasVisa);
            if (hasVisa != null)
                query = query.Where(x => x.HasVisa == hasVisa.Value);
            var passengers = Db.Select(query.SelectDistinct(x => x));

            return new TourPassengers { Tour = tour, Leader = leader, Passengers = passengers };
        }
    }
}
