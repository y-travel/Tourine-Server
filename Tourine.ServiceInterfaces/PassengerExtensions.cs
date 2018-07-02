using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourine.ServiceInterfaces.Models;

namespace Tourine.ServiceInterfaces
{
    public static class PassengerExtensions
    {
        public static string GetBuyerName(this PassengerInfo passenger, Guid currentTourId, IDbConnection db) =>
            passenger.TourId == currentTourId 
            ? passenger.Person.DisplayTitle 
            : passenger.TourId.GetAgency(db)?.DisplayTitle;
    }
}
