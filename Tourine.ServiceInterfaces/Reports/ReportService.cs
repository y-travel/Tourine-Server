using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using ServiceStack;
using ServiceStack.Web;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Reports.Data;

namespace Tourine.ServiceInterfaces.Reports
{
    public class ReportService : AppService
    {
        public object Get(GetReportFile req)
        {
            return new ReportFactory(Db, req.ReportType).GetReport(req.TourId);
        }

        public object Get(GetReportData getReportData)
        {
            return getReportData.ReportType.ToReportData(Db).FillData(getReportData.TourId);
        }
    }

    public class ReportFactory
    {
        public IDbConnection Db { get; }
        private ReportType ReportType { get; }
        public ReportFactory(IDbConnection db, ReportType reportType)
        {
            Db = db;
            ReportType = reportType;
        }

        public object GetReport(Guid tourId)
        {
            var report = ReportType.ToReportFile();
            report.DataSource = new object[] { ReportType.ToReportData(Db).FillData(tourId) };
            if (report.Parameters["reportDate"] != null)
                report.Parameters["reportDate"].Value = DateTime.Now.ToPersianDate();
            return report.GetPdfResult($"{report.GetType().Name}FileName".Loc());
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
