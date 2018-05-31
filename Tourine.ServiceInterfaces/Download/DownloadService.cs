using System;
using System.IO;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Download
{
    public class DownloadService : AppService
    {
        public object Get(GetTicketReportTemplate req)
        {
            var fileFullPath = $@"{PublicExtensions.GetRootDirectory()}\bin\tourinetemplate1.xlsx";
//            const string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//            var fi = new FileInfo(fileFullPath);
//
//            var reportBytes = File.ReadAllBytes(fi.FullName);
//            var result = new HttpResult(reportBytes, mimeType);
//            result.Headers.Add("Content-Disposition", "attachment; filename=\"" + "ticketReport" + ".xlsx\";");
//            return result;
            var fs = new FileStream(fileFullPath, FileMode.Open);
            return new FileStreamResult(fs);
        }
    }
}
