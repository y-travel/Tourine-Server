using ServiceStack;

namespace Tourine.ServiceInterfaces.Download
{
    [Route("/download/ticketReportTemplate", "GET")]
    public class GetReportTemplate : IReturn<HttpResult>
    {
        public ReportType ReportType { get; set; }
    }
    public enum ReportType
    {
        Tour = 1,
        Ticket = 2,
    }
}