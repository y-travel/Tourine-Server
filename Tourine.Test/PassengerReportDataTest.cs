using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;
using Tourine.ServiceInterfaces.Reports;
using Tourine.ServiceInterfaces.Reports.Data;
using Tourine.Test.Common;

namespace Tourine.Test
{
    public class PassengerReportDataTest : ServiceTest<ReportService>
    {
        public Guid CurrentTourId = Guid.NewGuid();
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
            foreach (var testCase in PassengerTestCases)
            {
                new InsertHelper<Person>(Db, (Person)testCase[0])
                    .Insert(x => new Passenger { PersonId = x.Id, Person = x, TourId = tourId, OptionType = (OptionType)testCase[1] });
            }
        }
        [Test]
        public void FillData_should_be_fill_all_fields()
        {
            var reportData = new PassengerReportData(Db, CurrentTourId);

            reportData.PassengerCount.Should().Be(4);
            reportData.AdultCount.Should().Be(2);
            reportData.InfantCount.Should().Be(1);
            reportData.BedCount.Should().Be(1);
            reportData.FoodCount.Should().Be(4);
        }

    }
}
