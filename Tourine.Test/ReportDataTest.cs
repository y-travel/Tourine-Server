using System;
using System.Data;
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
        public static Guid CurrentTourId = Guid.NewGuid();
        public Guid SampleBlockId = Guid.NewGuid();
        public static IDbConnection CurrentDb;
        public static object[][] PassengerTestCases =
        {
            new object[] { new Person { IsInfant = false, IsUnder5 = false }, OptionType.Bus|OptionType.Food},
            new object[] { new Person { IsInfant = false, IsUnder5 = false }, OptionType.Food|OptionType.Room},
            new object[] { new Person { IsInfant = true, IsUnder5 = false }, OptionType.Food},
            new object[] { new Person { IsInfant = false, IsUnder5 = true }, OptionType.Food},
        };
        public static VisaReportData CreateVisaReportData() => (VisaReportData)new VisaReportData(CurrentDb).FillData(CurrentTourId);
        public static TicketReportData CreateTicketReportData() => (TicketReportData)new TicketReportData(CurrentDb).FillData(CurrentTourId);
        public static PassengerReportData CreatePassengerReportData() => (PassengerReportData)new PassengerReportData(CurrentDb).FillData(CurrentTourId);

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            CurrentDb = Db;
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

        [Category("PassengerReport")]
        [Test]
        public void PassengerReport_should_fill_all_fields()
        {
            var reportData = CreatePassengerReportData();

            reportData.PassengersInfos.Count.Should().Be(PassengerTestCases.Length);
            reportData.AdultCount.Should().Be(2);
            reportData.InfantCount.Should().Be(1);
            reportData.BedCount.Should().Be(1);
            reportData.FoodCount.Should().Be(4);
        }

        [Category("TicketReport")]
        [Test]
        public void TicketReport_should_fill_all_fields()
        {
            var tour = Db.Select<Tour>(x => x.Id == CurrentTourId).FirstOrDefault();
            var reportData = CreateTicketReportData();

            reportData.PassengersInfos.Count.Should().Be(PassengerTestCases.Length);
            reportData.TourDetail.Id.Should().Be(tour.TourDetailId.Value);
            reportData.AdultCount.Should().Be(2);
            reportData.InfantCount.Should().Be(1);

        }

        [Category("VisaReport")]
        [Test]
        public void VisaReport_should_fill_all_fields()
        {
            var block = Db.Select<Tour>(x => x.Id == SampleBlockId).FirstOrDefault();
            block.Agency = new InsertHelper<Agency>(Db, new Agency()).Result;
            Db.Save(block, true);
            var reportData = CreateVisaReportData();

            reportData.TourDetail.Id.Should().Be(block.TourDetailId.Value);
            reportData.PassengersInfos.Count.Should().Be(PassengerTestCases.Length);
            reportData.BuyerNames.Count.Should().Be(PassengerTestCases.Length);
            reportData.BuyerNames.ContainsValue(block.Agency.DisplayTitle).Should().BeTrue();
        }

        [Category("VisaReport"),Description("گزارش ویزا باید مسافرین بدون ویزا را برگرداند")]
        [Test]
        public void should_return_passengers_without_visa()
        {
            var reportData = CreateVisaReportData();
            new InsertHelper<Person>(Db, new Person()).Insert(x => new Passenger { PersonId = x.Id, HasVisa = true });
            reportData.PassengersInfos.Count.Should().Be(PassengerTestCases.Length);
        }

        [Category("TicketReport")]
        [Category("PassengerReport")]
        [Description("در گزارش بلیط و مسافر تور سرگروه باید در اول لیست باشد")]
        [Test]
        public void should_return_leader_as_first_passenger()
        {
            var leaderId = new InsertHelper<Person>(Db, new Person()).Result.Id;
            UpdateCurrentTourLeader(leaderId);

            CreateTicketReportData().PassengersInfos[0].PersonId.Should().Be(leaderId);
            CreateTicketReportData().PassengersInfos[0].PersonId.Should().Be(leaderId);
        }
        [Category("TicketReport")]
        [Category("PassengerReport")]
        [Description("اگر سرگروه بعنوان مسافر ثبت شده بود آنرا در اول لیست برگرداند")]
        [Test]
        public void should_detect_leader_from_passenger()
        {
            var leaderId = ((Person)PassengerTestCases[2][0]).Id;
            UpdateCurrentTourLeader(leaderId);

            CreateTicketReportData().PassengersInfos[0].PersonId.Should().Be(leaderId);
            CreatePassengerReportData().PassengersInfos[0].PersonId.Should().Be(leaderId);
        }

        public void UpdateCurrentTourLeader(Guid leaderId)
        {
            var tour = Db.LoadSelect<Tour>(x => x.Id == CurrentTourId).SingleOrDefault();
            tour.TourDetail.LeaderId = leaderId;
            Db.Update(tour.TourDetail);
        }
    }
}
