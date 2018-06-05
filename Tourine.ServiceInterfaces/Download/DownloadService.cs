using System;
using System.IO;
using System.Web;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Download
{
    public class DownloadService : AppService
    {
        public object Get(GetTicketReportTemplate req)
        {
            var fileFullPath = $@"{PublicExtensions.GetRootDirectory()}\bin\tourinetemplate1.xlsx";
            return new HttpResult(new FileInfo(fileFullPath), MimeMapping.GetMimeMapping(fileFullPath));
        }
    }
}
