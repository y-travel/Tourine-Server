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

        private object GetTicketReport(Guid tourId) =>
            GenerateReport(new TicketReport { DataSource = new object[] { new TicketReportData(Db).FillData(tourId) } });

        private object GetVisaReport(Guid tourId) =>
            GenerateReport(new VisaReport { DataSource = new object[] { new VisaReportData(Db).FillData(tourId) } });

        private object GetTourPassengersReport(Guid tourId) =>
            GenerateReport(new PassengerReport { DataSource = new object[] { new PassengerReportData(Db).FillData(tourId) } });

        private object GenerateReport(XtraReport report)
        {
            report.Parameters["reportDate"].Value = DateTime.Now.ToPersianDate();
            return report.GetPdfResult(Strings.PassengerReportFileName);
        }

    }

    public static class ReportExtensions
    {
        public static object GetPdfResult(this XtraReport report, string filename)
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
