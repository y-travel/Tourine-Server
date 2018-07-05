using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraReports.UI;
using ServiceStack;
using ServiceStack.Web;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Reports.Data;

namespace Tourine.ServiceInterfaces.Reports
{
    public class ReportService : AppService
    {
        public object Get(GetReport req)
        {
            return new ReportFactory(Db).GetReport(req);
        }
    }

    public class ReportFactory
    {
        public IDbConnection Db { get; }

        public ReportFactory(IDbConnection db)
        {
            Db = db;
        }

        public object GetReport(GetReport request)
        {
            switch (request.ReportType)
            {
                case ReportType.TourPassenger:
                    return GetTourPassengersReport(request.TourId);
                case ReportType.Ticket:
                    return GetTicketReport(request.TourId);
                case ReportType.Visa:
                    return GetVisaReport(request.TourId);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private object GetTicketReport(Guid tourId)
        {
            var report = new TicketReport { DataSource = new object[] { new TicketReportData(Db, tourId) } };
            report.Parameters["reportDate"].Value = DateTime.Now.ToPersianDate();
            return GetPdfResult(report, Strings.TicketReportFileName);
        }

        private object GetVisaReport(Guid tourId)
        {
            var report = new VisaReport { DataSource = new object[] { new VisaReportData(Db, tourId) } };
            report.Parameters["reportDate"].Value = DateTime.Now.ToPersianDate();
            return GetPdfResult(report, Strings.VisaReportFileName);
        }

        public object GetTourPassengersReport(Guid tourId)
        {
            var report = new PassengerReport { DataSource = new object[] { new PassengerReportData(Db, tourId) } };
            report.Parameters["reportDate"].Value = DateTime.Now.ToPersianDate();
            return GetPdfResult(report, Strings.PassengerReportFileName);
        }

        private object GetPdfResult(XtraReport report, string filename)
        {
            var memoryStream = new MemoryStream();
            report.ExportToPdf(memoryStream);
            return new PdfResult(memoryStream, filename);
        }
    }

    public class PdfResult : IHasOptions, IStreamWriterAsync
    {
        public Stream ResponseStream;
        public IDictionary<string, string> Options { get; }

        public PdfResult(Stream responseStream, string defaultName = "pdf")
        {
            ResponseStream = responseStream;
            Options = new Dictionary<string, string>
            {
                {"Content-Type","application/pdf" },
                {"Content-Disposition", $"attachment; filename={defaultName}.pdf" }
            };
        }
        public Task WriteToAsync(Stream responseStream, CancellationToken token = new CancellationToken())
        {
            return Task.Run(() =>
            {
                if (ResponseStream == null)
                    return;
                ResponseStream.WriteTo(responseStream);
                responseStream.Flush();
                responseStream.Dispose();
            }, token);
        }
    }
}
