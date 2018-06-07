using System;
using System.IO;
using System.Web;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Download
{
    public class DownloadService : AppService
    {
        public object Get(GetReportTemplate req)
        {
            var templatePath = Path.Combine(PathHelper.GetAssetsPath(), $"{Enum.GetName(typeof(ReportType), req.ReportType)}Template.xlsx");
            return new HttpResult(new FileInfo(templatePath), MimeMapping.GetMimeMapping(templatePath));
        }
    }
}
