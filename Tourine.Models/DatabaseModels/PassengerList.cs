﻿using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class PassengerList
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [References(typeof(Block))]
        public Guid BlockId { get; set; }
        [Reference]
        public Block Block { get; set; }

        [References(typeof(Passenger))]
        public Guid PassengerId { get; set; }
        [Reference]
        public Passenger Passenger { get; set; }
    }
}
