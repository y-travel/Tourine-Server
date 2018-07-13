using System;
using System.Data;
using DevExpress.XtraReports.UI;
using FluentAssertions;
using NUnit.Framework;
using Telerik.JustMock;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Reports;
using Tourine.ServiceInterfaces.Reports.Data;
using Tourine.Test.Common;

namespace Tourine.Test.Services
{
    public class ReportServiceTest : ServiceTest<ReportService>
    {
        private void MockReportEnvironmnet(ReportType reportType)
        {
            Mock.SetupStatic(typeof(ReportTypeExtensions));
            Mock.SetupStatic(typeof(ReportExtensions), Behavior.Strict, StaticConstructor.Mocked);
            Mock.Arrange(() => reportType.ToReportData(Db)).Returns(new MockReportData(Db));
            var mockReport = new XtraReport();
            Mock.Arrange(() => reportType.ToReportFile()).Returns(mockReport);
            Mock.Arrange(() => mockReport.GetPdfResult("XtraReportFileName".Loc())).Returns(true);

        }
        [Test]
        [TestCase(ReportType.TourPassenger)]
        [TestCase(ReportType.Ticket)]
        [TestCase(ReportType.Visa)]
        public void GetReport_should_return_correct_report_name(object reportType)
        {
            var req = new GetReportFile { ReportType = (ReportType)reportType };
            MockReportEnvironmnet(req.ReportType);
            MockService.Get(req).Should().Be(true);
        }

        public class MockReportData : PassengerDataReportBase
        {
            public MockReportData(IDbConnection db) : base(db)
            {
            }

            public override PassengerDataReportBase FillData(Guid tourId)
            {
                return this;
            }
        }


    }
}
