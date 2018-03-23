using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Services;
using Tourine.ServiceInterfaces.TourDetails;

namespace Tourine.ServiceInterfaces.Tours
{
    public class Tour
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Capacity { get; set; }
        public int BasePrice { get; set; }

        [References(typeof(Tour))]
        public Guid? ParentId { get; set; }
        [Reference]
        public Tour Parent { get; set; }

        public string Code { get; set; }
        public TourStatus Status { get; set; } = TourStatus.Created;

        [References(typeof(TourDetail))]
        public Guid? TourDetailId { get; set; }
        [Reference]
        public TourDetail TourDetail { get; set; }

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        public long InfantPrice { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Ignore]
        public List<TourOption> Options { get; set; }

    }

    public static class TourExtensions
    {
        public static int getBlocksCapacity(this Tour tour, IDbConnection Db)
        {
            var tourReserved = Db.Scalar<Tour, int>(
                t => Sql.Sum(t.Capacity),
                t => t.ParentId == tour.Id
            );
            return tourReserved;
        }
        public static int getCurrentPassengerCount(this Tour tour, IDbConnection Db)
        {
            var count = Db.Scalar<PassengerList, int>(x => Sql.CountDistinct(x.PersonId), x => x.TourId == tour.Id);
            return count;
        }

    }
}
