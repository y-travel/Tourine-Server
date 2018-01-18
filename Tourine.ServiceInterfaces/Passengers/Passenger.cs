﻿using System;
using ServiceStack.DataAnnotations;

namespace Tourine.ServiceInterfaces.Passengers
{
    public class Passenger
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Family { get; set; }
        public string MobileNumber { get; set; }
        public string NationalCode { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime PassportExpireDate { get; set; }

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        public string PassportNo { get; set; }
    }
}