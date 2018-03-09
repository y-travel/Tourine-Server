using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.AgencyPersons;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.ServiceInterfaces.Notifies
{
    public class NotifyService : AppService
    {
        public TourineBot bot { get; set; }

        public void Post(SendNotifyToTourAgencies agency)
        {
            var q = Db.From<Tour>()
                .Join<AgencyPerson>((tour, ap) =>
                    tour.AgencyId == ap.AgencyId && (tour.Id == agency.TourId || tour.ParentId == agency.TourId))
                .Join<AgencyPerson, Person>((ap, p) => ap.PersonId == p.Id)
                .Join<Person, User>((p, u) => p.Id == u.PersonId && u.Role == agency.Role)
                .Where<Person>(p => p.ChatId != null)
                .GroupBy<Person>(p => new { p.Id, p.ChatId, p.Name, p.Family })
                .Select<Person>(p => new { p.Id, p.ChatId, p.Name, p.Family });
            var persons = Db.Select<Person>(q);
            foreach (var person in persons)
                bot.Send(agency.Msg, person.ChatId);
        }

        public void Post(SendNotifyToTourBuyers tourBuyers)
        {
            var q = Db.From<Person, Team>((p, t) => t.TourId == tourBuyers.TourId && t.BuyerId == p.Id)
                .GroupBy(p => new { p.Id, p.ChatId, p.Name, p.Family })
                .Select<Person>(p => new { p.Id, p.ChatId, p.Name, p.Family });
            var buyers = Db.Select(q);
            foreach (var buyer in buyers)
                bot.Send(tourBuyers.Msg, buyer.ChatId);
        }

        public void Post(SendNotifyToTourPassengers tour)
        {
            var q = Db.From<Person, Services.PassengerList>((p, s) => s.TourId == tour.TourId && p.Id == s.PersonId)
                    .GroupBy(p => new { p.Id, p.ChatId, p.Name, p.Family })
                    .Select<Person>(p => new { p.Id, p.ChatId, p.Name, p.Family });
            var passengers = Db.Select(q);
            foreach (var passenger in passengers)
                bot.Send(tour.Msg, passenger.ChatId);
        }

        public void Post(SendNotifyToTourLeader tour)
        {
            var q = Db.From<TourDetail, Tour>((td, t) => t.TourDetailId == td.Id && t.Id == tour.TourId)
                .Join<Person>((td, p) => td.LeaderId == p.Id)
                .GroupBy<Person>(p => new { p.Id, p.ChatId, p.Name, p.Family })
                .Select<Person>(p => new { p.Id, p.ChatId, p.Name, p.Family });
            var leaders = Db.Select<Person>(q);
            foreach (var leader in leaders)
                bot.Send(tour.Msg, leader.ChatId);
        }
    }
}