using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Reports
{
    [Route("/reports/{ReportType}", "GET")]
    public class GetReport : IReturn<HttpResult>
    {
        public Guid TourId { get; set; }
        public ReportType ReportType { get; set; }
    }
    public enum ReportType
    {
        TourPassenger = 1,
        Ticket = 2,
        Visa = 3,
    }
}