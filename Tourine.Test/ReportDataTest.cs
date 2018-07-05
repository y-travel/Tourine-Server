using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;
using Tourine.ServiceInterfaces.Reports;
using Tourine.ServiceInterfaces.Reports.Data;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class ReportDataTest : ServiceTest<ReportService>
    {
        public Guid CurrentTourId = Guid.NewGuid();
        public Guid SampleBlockId = Guid.NewGuid();
        public static object[][] PassengerTestCases =
        {
            new object[] { new Person { IsInfant = false, IsUnder5 = false }, OptionType.Bus|OptionType.Food},
            new object[] { new Person { IsInfant = false, IsUnder5 = false }, OptionType.Food|OptionType.Room},
            new object[] { new Person { IsInfant = true, IsUnder5 = false }, OptionType.Food},
            new object[] { new Person { IsInfant = false, IsUnder5 = true }, OptionType.Food},
        };

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            FillDb(CurrentTourId);
        }
        private void FillDb(Guid tourId)
        {
            new InsertHelper<TourDetail>(Db, new TourDetail())
                .Insert(x => new Tour { Id = tourId, TourDetailId = x.Id, TourDetail = x })
                .Insert(x => new Tour { Id = SampleBlockId, TourDetailId = x.TourDetailId, ParentId = tourId });
            foreach (var testCase in PassengerTestCases)
            {
                new InsertHelper<Person>(Db, (Person)testCase[0])
                    .Insert(x => new Passenger { PersonId = x.Id, Person = x, TourId = SampleBlockId, OptionType = (OptionType)testCase[1] });
            }
        }
        [Test]
        public void PassengerReport_should_be_fill_all_fields()
        {
            var reportData = (PassengerReportData)new PassengerReportData(Db).FillData(CurrentTourId);

            reportData.PassengersInfos.Count.Should().Be(PassengerTestCases.Length);
            reportData.PassengerCount.Should().Be(4);
            reportData.AdultCount.Should().Be(2);
            reportData.InfantCount.Should().Be(1);
            reportData.BedCount.Should().Be(1);
            reportData.FoodCount.Should().Be(4);
        }

        [Test]
        public void TicketReport_should_be_fill_all_fields()
        {
            var tour = Db.Select<Tour>(x => x.Id == CurrentTourId).FirstOrDefault();
            var reportData = (TicketReportData)new TicketReportData(Db).FillData(CurrentTourId);

            reportData.PassengersInfos.Count.Should().Be(PassengerTestCases.Length);
            reportData.TourDetail.Id.Should().Be(tour.TourDetailId.Value);
            reportData.AdultCount.Should().Be(2);
            reportData.InfantCount.Should().Be(1);

        }

        [Test]
        public void VisaReport_should_be_fill_all_fields()
        {
            var block = Db.Select<Tour>(x => x.Id == SampleBlockId).FirstOrDefault();
            block.Agency = new InsertHelper<Agency>(Db, new Agency()).Result;
            Db.Save(block, true);
            var reportData = (VisaReportData)new VisaReportData(Db).FillData(CurrentTourId);

            reportData.TourDetail.Id.Should().Be(block.TourDetailId.Value);
            reportData.PassengersInfos.Count.Should().Be(PassengerTestCases.Length);
            reportData.BuyerNames.Count.Should().Be(PassengerTestCases.Length);
            reportData.BuyerNames.ContainsValue(block.Agency.DisplayTitle).Should().BeTrue();
        }
    }
}
