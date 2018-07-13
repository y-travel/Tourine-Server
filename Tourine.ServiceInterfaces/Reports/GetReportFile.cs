using System;
using System.Data;
using DevExpress.XtraReports.UI;
using ServiceStack;
using Tourine.ServiceInterfaces.Reports.Data;

namespace Tourine.ServiceInterfaces.Reports
{
    [Route("/reports/{ReportType}/file", "GET")]
    public class GetReportFile : IReturn<HttpResult>
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

    public static class ReportTypeExtensions
    {
        public static PassengerDataReportBase ToReportData(this ReportType reportType, IDbConnection db)
        {
            switch (reportType)
            {
                case ReportType.TourPassenger:
                    return new PassengerReportData(db);
                case ReportType.Ticket:
                    return new TicketReportData(db);
                case ReportType.Visa:
                    return new VisaReportData(db);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static XtraReport ToReportFile(this ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.TourPassenger:
                    return new PassengerReport();
                case ReportType.Ticket:
                    return new TicketReport();
                case ReportType.Visa:
                    return new VisaReport();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


    }
}