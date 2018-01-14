using System.Web.Hosting;
using ServiceStack;
using ServiceStack.Host.Handlers;
using ServiceStack.Web;

namespace Tourine.Common
{
    public class CustomStaticFileHandler: HttpAsyncTaskHandler
    {
        public string FilePath { get; }

        public CustomStaticFileHandler(string filePath)
        {
            FilePath = filePath;
        }

        public override void ProcessRequest(IRequest request, IResponse response, string operationName)
        {
            response.EndHttpHandlerRequest(skipClose:true,afterHeaders: res =>
            {
                var file = VirtualPathProvider.OpenFile(FilePath);
                
                res.SetContentLength(file.Length);

                var output = res.OutputStream;
                file.CopyTo(output);
                output.Flush();
            });
        }
    }
}