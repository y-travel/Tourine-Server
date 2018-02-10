﻿using System;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Passengers;
using Tourine.ServiceInterfaces.Tours;

namespace Tourine.ServiceInterfaces.Services
{
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Passenger))]
        public Guid PassengerId { get; set; }
        [Reference]
        public Passenger Passenger { get; set; }

        [References(typeof(Tour))]
        public int TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public ServiceType Type { get; set; }
        public ServiceStatus Status { get; set; }
    }
}