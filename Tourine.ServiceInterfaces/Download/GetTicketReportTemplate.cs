using System.Collections.Generic;
using System.IO;
using ServiceStack;
using ServiceStack.Web;

namespace Tourine.ServiceInterfaces.Download
{
    [Route("/download/ticketReportTemplate","GET")]
    public class GetTicketReportTemplate : IReturn<HttpResult>
    {
    }

//    public class FileStreamResult : IHasOptions, IStreamWriter
//    {
//        private readonly Stream _responseStream;
//        public IDictionary<string, string> Options { get; }
//
//        public FileStreamResult(Stream responseStream, string fileName)
//        {
//            _responseStream = responseStream;
//            Options = new Dictionary<string, string>
//            {
//                {"Content-Type","application/octet-stream" },
//                {"Content-Disposition","attachment; filename=\"" + fileName + "\";" }
//            };
//        }
//
//        public void WriteTo(Stream responseStream)
//        {
//            if ( _responseStream == null)
//                return;
//            _responseStream.WriteTo(responseStream);
//            responseStream.Flush();
//            responseStream.Dispose();
//        }
//    }
}
