using System;
using ServiceStack.DataAnnotations;

namespace Tourine.Models.DatabaseModels
{
    public class AgencyCustomer
    {
        [AutoIncrement]
        public Guid Id { get; set; }

        [References(typeof(Agency))]
        public Guid AgencyId { get; set; }
        [Reference]
        public Agency Agency { get; set; }

        [References(typeof(Customer))]
        public Guid CustomerId { get; set; }
        [Reference]
        public Customer Customer { get; set; }
    }
}
