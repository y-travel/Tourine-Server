using System.Collections.Generic;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Reports.Data
{
    public class PassengerReportData
    {
        public int PassengerCount { get; set; }

        public int AdultCount { get; set; }

        public int InfantCount { get; set; }

        public int BedCount { get; set; }

        public int FoodCount { get; set; }
        public List<TeamMember> Tours { get; set; }
    }
}
