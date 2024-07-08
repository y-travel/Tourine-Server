using System;
using ServiceStack;
using Tourine.ServiceInterfaces.Reports.Data;

namespace Tourine.ServiceInterfaces.Reports
{
    [Route("/reports/{ReportType}/data", "GET")]
    public class GetReportData : IReturn<PassengerDataReportBase>
    {
        public Guid TourId { get; set; }
        public ReportType ReportType { get; set; }
    }
}