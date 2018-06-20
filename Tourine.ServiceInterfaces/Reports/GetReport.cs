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
        Tour = 1,
        Ticket = 2,
    }
}