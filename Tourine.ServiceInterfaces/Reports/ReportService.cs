using System;
using System.Collections.Generic;
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
            var templatePath = Path.Combine(PathHelper.GetAssetsPath(), $"{Enum.GetName(typeof(ReportType), req.ReportType)}Template.xlsx");
            return new HttpResult(new FileInfo(templatePath), MimeMapping.GetMimeMapping(templatePath));
        }
    }

    public class ReportFactory
    {
        public static HttpResult GetTourPassengersReport(PassengerReportData data)
        {
            var report = new PassengerReport();
            report.DataSource = data;
            var memoryStream = new MemoryStream();
            report.ExportToPdf(memoryStream);
            return new HttpResult(new PdfResult(memoryStream));
        }
    }

    public class PdfResult : IHasOptions, IStreamWriterAsync
    {
        private readonly Stream _responseStream;
        public IDictionary<string, string> Options { get; }

        public PdfResult(Stream responseStream, string defaultName = "pdf")
        {
            _responseStream = responseStream;
            Options = new Dictionary<string, string>
            {
                {"Content-Type","application/pdf" },
                {"Content-Disposition", $"attachment; filename={defaultName}.pdf\";" }
            };
        }

        public Task WriteToAsync(Stream responseStream, CancellationToken token = new CancellationToken())
        {
            return Task.Run(() =>
            {
                if (_responseStream == null)
                    return;
                _responseStream.WriteTo(responseStream);
                responseStream.Flush();
                responseStream.Dispose();
            }, token);
        }
    }
}
