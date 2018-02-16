using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Teams
{
    public class Team
    {
        public Guid Id { get; set; }

        [References(typeof(Tour))]
        public Guid TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public int Count { get; set; }
        public int  Price { get; set; }
        public DateTime SubmitDate { get; set; }

        [References(typeof(Person))]
        public Guid LeaderId { get; set; }
        [Reference]
        public Person Leader { get; set; }

        [References(typeof(Person))]
        public Guid BuyerId { get; set; }
        [Reference]
        public Person Buyer { get; set; }
    }
}
