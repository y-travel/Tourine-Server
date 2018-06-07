using System.IO;
using NUnit.Framework;
using ServiceStack;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Download;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class DownloadServiceTest : ServiceTest<DownloadService>
    {
        [Test, TestCase(ReportType.Tour), TestCase(ReportType.Ticket)]
        public void GetReportTemplate_should_return_correct_template(object reportType)
        {
            //init
            Directory.CreateDirectory(PathHelper.GetAssetsPath());
            var expectedTemplatePath = Path.Combine(PathHelper.GetAssetsPath(), $"{reportType}Template.xlsx");
            File.Create(expectedTemplatePath).Close();
            //act
            var req = new GetReportTemplate { ReportType = (ReportType)reportType };
            var result = (HttpResult)MockService.Get(req);
            //assert: should be pass test without error
            Assert.AreEqual(expectedTemplatePath, result.FileInfo.FullName);
            //clean up
            File.Delete(expectedTemplatePath);
        }
    }
}
