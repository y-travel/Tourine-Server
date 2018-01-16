using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class Block
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }

        [References(typeof(Tour))]
        public Guid TourId { get; set; }
        [Reference]
        public Tour Tour { get; set; }

        public int Price { get; set; }
        public int Capacity { get; set; }

        [References(typeof(Block))]
        public Guid? ParentId { get; set; }
        [Reference]
        public Block Parent { get; set; }

        [References(typeof(Customer))]
        public Guid CustomerId { get; set; }
        [Reference]
        public Customer Customer { get; set; }

        public DateTime SubmitDate { get; set; }
    }
}
