using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class PassengerList
    {
        [AutoIncrement]
        public Guid Id { get; set; }

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
