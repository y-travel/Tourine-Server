using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Services;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces
{
    public partial class TourineBotCmdService
    {
        private IDbConnectionFactory ConnectionFactory { get; set; }
        public TourineBotCmdService(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public bool IsRegistered(Message message)
        {
            //@TODO: first check from sesion if not exist then retreve from DB 
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                return db.Exists<Person>(x => x.ChatId == message.Chat.Id);
            }
        }

        public TourineCmdStatus Register(Message message)
        {
            if (message.Type != MessageType.ContactMessage)
                return TourineCmdStatus.ContactTypeError;

            using (var db = ConnectionFactory.OpenDbConnection())
            {
                if (!db.Exists<Person>(x => x.SocialNumber == message.Contact.PhoneNumber))
                    return TourineCmdStatus.NumberError;
                var persons = db.Select<Person>().Where(x => x.SocialNumber == message.Contact.PhoneNumber);
                if (persons.Count() > 1)
                    return TourineCmdStatus.MultipleNumber;
                var person = persons.ToArray()[0];
                person.ChatId = message.Chat.Id;
                db.Update(person);
                return TourineCmdStatus.Registered;
            }
        }

        public PersonType RegisteredAs(Message message)
        {
            //@TODO: first check from sesion if not exist then retreve from DB
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                return db.Single<Person>(x => x.ChatId == message.Chat.Id).Type;
            }
        }

        public Dictionary<ServiceType, int> NumberOfTourServices(Guid tourId)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                var q = db.From<Service>()
                    .Where(s => s.TourId == tourId)
                    .GroupBy(x => x.Type)
                    .Select(x => new { x.Type, serviceCount = Sql.Count("*") });
                var results = db.Dictionary<ServiceType, int>(q);
                return results;
            }
        }

        public Dictionary<ServiceType, int> NumberOfTourUnder5Services(Guid tourId)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                var q = db.From<Service>()
                    .Join<Person>((s, p) => s.PersonId == p.Id && (p.IsInfant || p.IsUnder5))
                    .Where(s => s.TourId == tourId)
                    .GroupBy(x => x.Type)
                    .Select(x => new { x.Type, serviceCount = Sql.Count("*") });
                var results = db.Dictionary<ServiceType, int>(q);
                return results;
            }
        }

        public Tour GetLeaderLastRunningTour(Guid leaderId)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                var q = db.From<Tour, TourDetail>(
                    (tour1, detail) => tour1.TourDetailId == detail.Id
                                       && tour1.Status == TourStatus.Running
                                       && detail.LeaderId == leaderId).OrderByDescending<TourDetail>(x => x.StartDate).Take(1);
                var tour = db.Single(q);

                return FillTour(tour.Id);
            }
        }

        public Tour FillTour(Guid tourId)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                var tour = db.SingleById<Tour>(tourId);
                tour.Agency = db.SingleById<Agency>(tour.AgencyId);
                tour.TourDetail = db.SingleById<TourDetail>(tour.TourDetailId);
                tour.TourDetail.Destination = db.SingleById<Destination>(tour.TourDetail.DestinationId);
                return tour;
            }
        }

        public List<PersonInfo> GetTourPersonList(Guid tourId)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                var tourDetail = db.Single(db.From<TourDetail, Tour>((detail, tour) =>
                    detail.Id == tour.TourDetailId && tour.Id == tourId));
                var leader = db.SingleById<Person>(tourDetail.LeaderId);

                var q = db.From<Person, Service>((p, s) => p.Id == s.PersonId && s.TourId == tourId)
                    .GroupBy<Person>(person => new { person.Id, person.Family, person.Name, person.IsInfant, person.IsUnder5, person.Gender })
                    .OrderBy<Person>(person => new { person.Family, person.Name })
                    .Select(p => new { p.Family, p.Name, p.IsInfant, p.IsUnder5, p.Gender });
                var items = db.Select<PersonInfo>(q);
                items.Insert(0, new PersonInfo { Name = leader.Name, Family = leader.Family, Gender = leader.Gender, IsInfant = leader.IsInfant, IsUnder5 = leader.IsUnder5, IsLeader = true });
                return items;
            }
        }

        public List<PersonServiceReport> TourPersonListAndService(Guid tourId)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                var q = db.From<Person, Service>((p, s) => p.Id == s.PersonId && s.TourId == tourId)
                    .GroupBy<Person>(person => new { person.Id, person.Family, person.Name })
                    .OrderBy<Person>(person => new { person.Family, person.Name })
                    .Select(x => new { x.Family, x.Name, serviceSum = Sql.Sum("Service.Type") });
                var item = db.Select<PersonServiceReport>(q);
                return item;
            }
        }

        public List<PersonServiceReport> GetTourUnder5PersonListAndService(Guid TourId)
        {
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                var q = db.From<Person, Service>((p, s) => p.Id == s.PersonId && s.TourId == TourId && (p.IsInfant || p.IsUnder5))
                    .GroupBy<Person>(person => new { person.Id, person.Family, person.Name })
                    .OrderBy<Person>(person => new { person.Family, person.Name })
                    .Select(x => new { x.Family, x.Name, serviceSum = Sql.Sum("Service.Type") });
                var item = db.Select<PersonServiceReport>(q);
                return item;
            }
        }
        public TourInfo GetLeaderLastRunnungTourServiceCount(Guid leaderId)
        {
            var tour = GetLeaderLastRunningTour(leaderId);
            var services = NumberOfTourServices(tour.Id);

            var t = new TourInfo {Tour = tour};
            t.Tour.TourDetail = tour.TourDetail;
            foreach (var s in services)
            {
                t.SerivceCounts.Add(new TourInfo.SerivceCount { ServiceType = s.Key, ServiceCount = s.Value });
            }
            return t;
        }

        public TourInfo GetLeaderLastRunnungTourServiceCountForUnder5(Guid leaderId)
        {
            var tour = GetLeaderLastRunningTour(leaderId);
            var services = NumberOfTourUnder5Services(tour.Id);

            var t = new TourInfo {Tour = tour};
            foreach (var s in services)
            {
                t.SerivceCounts.Add(new TourInfo.SerivceCount { ServiceType = s.Key, ServiceCount = s.Value });
            }
            return t;
        }

        public class TourInfo
        {
            public class SerivceCount
            {
                public ServiceType ServiceType { get; set; }
                public int ServiceCount { get; set; }
            }
            public Tour Tour { get; set; }

            public readonly List<SerivceCount> SerivceCounts = new List<SerivceCount>();

            public string GetTelegramView()
            {
                string dest = Tour.TourDetail.Destination.Name;
                string startDate = Tour.TourDetail.StartDate.ToString("yyyy/MM/dd");
                string servicesStr = "";
                foreach (var service in SerivceCounts)
                {
                    servicesStr += " " + service.ServiceType.GetEmoji() + service.ServiceType.GetDescription() + " : " + service.ServiceCount + " نفر " + "\r\n";
                }
                string stringView = "مقصد:" + dest + "\r\n" + "تاریخ:" +
                    startDate + "\r\n" + "سرویس ها:" + "\r\n" + servicesStr;
                return stringView;
            }
        }

        public class PersonInfo
        {
            public string Family { get; set; }
            public string Name { get; set; }
            public bool IsInfant { get; set; }
            public bool IsUnder5 { get; set; }
            public bool Gender { get; set; }
            public bool IsLeader { get; set; }
            public string GetTelegramView()
            {
                var res = "";
                if (IsInfant)
                    res += "👶🏻";
                else if (IsUnder5 && Gender)
                    res += "👦🏻";
                else if (IsUnder5 && !Gender)
                    res += "👧🏻";
                else if (!Gender)
                    res += "👩🏻";
                else if (Gender)
                    res += "👨🏻";
                else
                    res += "🌈";  

                res += " " + Family + "," + Name + " ";
                if (IsInfant)
                {
                    res += "(اینفنت)";
                }
                else if (IsUnder5)
                {
                    res += "(بدون خدمات)";
                }
                return res;
            }
        }

        public TourInfo GetTourServiceCountForUnder5(Guid tourId)
        {
            var services = NumberOfTourUnder5Services(tourId);
            var tour = FillTour(tourId);
            var t = new TourInfo { Tour = tour };
            foreach (var s in services)
            {
                t.SerivceCounts.Add(new TourInfo.SerivceCount { ServiceType = s.Key, ServiceCount = s.Value });
            }
            return t;
        }

        public TourInfo GetTourServiceCount(Guid tourId)
        {
            var services = NumberOfTourServices(tourId);
            var tour = FillTour(tourId);
            var t = new TourInfo { Tour = tour };
            foreach (var s in services)
            {
                t.SerivceCounts.Add(new TourInfo.SerivceCount { ServiceType = s.Key, ServiceCount = s.Value });
            }
            return t;
        }

        public bool IsLeader(long chatId)
        {
            return true;
        }
    }
}
