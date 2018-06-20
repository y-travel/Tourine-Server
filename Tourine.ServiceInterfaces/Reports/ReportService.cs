using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
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
            return ReportFactory.GetTourPassengersReport(Db, req.TourId, DateTime.Now);
        }
    }

    public class ReportFactory
    {
        public static object GetTourPassengersReport(IDbConnection db, Guid tourId, DateTime reportDate)
        {
            var report = new PassengerReport { DataSource = new object[] { new PassengerReportData(db, tourId) } };
            report.Parameters["reportDate"].Value = reportDate;
            var memoryStream = new MemoryStream();
            report.ExportToPdf(memoryStream);
            return new PdfResult(memoryStream);
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
