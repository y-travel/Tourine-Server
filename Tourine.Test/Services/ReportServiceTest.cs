using NUnit.Framework;
using Tourine.ServiceInterfaces.Reports;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class ReportServiceTest : ServiceTest<ReportService>
    {
        [Test, TestCase(ReportType.TourPassenger), TestCase(ReportType.Ticket)]
        public void GetReport_should_return_correct_report_name(object reportType)
        {
            var req = new GetReport { ReportType = (ReportType)reportType };
        }
    }
}
