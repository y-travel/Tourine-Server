using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using ServiceStack;
using ServiceStack.Web;

namespace Tourine.ServiceInterfaces.Download
{
    [Route("/download/ticketReportTemplate", "GET")]
    public class GetTicketReportTemplate : IReturn<HttpResult>
    {
    }

    public class FileResult : IHasOptions, IStreamWriterAsync
    {
        private readonly FileInfo _fileInfo;
        public IDictionary<string, string> Options { get; set; }

        public FileResult(FileInfo fileInfo)
        {
            var contentType = MimeMapping.GetMimeMapping(fileInfo.Name);
            _fileInfo = fileInfo;

            Options = new Dictionary<string, string>
            {
                {HttpHeaders.AcceptRanges, "bytes"},
                {HttpHeaders.ContentDisposition,$"attachment; filename={fileInfo.Name}" },
                {HttpHeaders.ContentType, contentType},
            };
        }

        public Task WriteToAsync(Stream responseStream, CancellationToken token = new CancellationToken())
        {
            return Task.Run(() =>
            {
                using (var fs = _fileInfo.OpenRead())
                {
                    fs.WriteTo(responseStream);
                }
            }, token);
        }
    }
}
