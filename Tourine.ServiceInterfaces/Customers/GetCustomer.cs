using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/{Id}", "GET")]
    public class GetCustomer : IReturn<Customer>
    {
        public Guid Id { get; set; }
    }
}
