using System;
using ServiceStack;
using ServiceStack.DataAnnotations;
using Tourine.ServiceInterfaces.Customers;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    [Route("/agencies/customers/", "GET")]
    public class GetCustomerOfAgency : QueryDb<Customer> , IJoin<Customer,AgencyCustomer>
    {
        [Ignore]
        public Guid? AgencyId { get; set; }
    }
}
