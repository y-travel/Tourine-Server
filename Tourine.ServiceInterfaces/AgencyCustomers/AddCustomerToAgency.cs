using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    [Route("/agencies/customers", "POST")]
    public class AddCustomerToAgency : IReturn<AgencyCustomer>
    {
        [Ignore]
        public Guid? AgencyId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
