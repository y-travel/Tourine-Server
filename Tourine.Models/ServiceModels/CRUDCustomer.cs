using System;
using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.Models.ServiceModels
{
    [Route("/customer/customer", "GET")]
    public class GetCustomer : IReturn<Customer>
    {
        public Guid Id { get; set; }
    }

    [Route("/customer/customers", "GET")]
    public class GetCustomers : QueryDb<Customer>
    {
    }

    [Route("/customer/customer", "POST")]
    public class PostCustomer : IReturn<Customer>
    {
        public Customer Customer { get; set; }
    }

    [Route("/customer/customer", "DELETE")]
    public class DeleteCustomer : IReturn<Customer>
    {
        public Guid Id { get; set; }
    }

    [Route("/customer/customer", "PUT")]
    public class PutCustomer : IReturn<Customer>
    {
        public Customer Customer { get; set; }
    }

}
