using System;
using System.Data;
using System.Linq;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public static class AgencyExtensions
    {
        //@Todo should be cache
        public static Agency GetAgency(this Guid tourId, IDbConnection db)
        {
            return db.Select<Agency>(
                    db.From((Tour tour, Agency agency) => tour.AgencyId == agency.Id)
                        .Where(x => x.Id == tourId)
                ).FirstOrDefault();
        }
    }
}
