﻿using System;
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
    }
}
