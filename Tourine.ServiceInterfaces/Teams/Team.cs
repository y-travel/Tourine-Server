using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Teams
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Tour))]
        public Guid TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public int Count { get; set; }
        public DateTime SubmitDate { get; set; } = DateTime.Now;

        [References(typeof(Person))]
        public Guid BuyerId { get; set; }
        [Reference]
        public Person Buyer { get; set; }

        public long InfantPrice { get; set; }
        public long BasePrice { get; set; }
        public long TotalPrice { get; set; }
        public bool BuyerIsPassenger { get; set; } = true;

        [Ignore]
        public List<TeamMember> Type { get; set; }
    }
}
