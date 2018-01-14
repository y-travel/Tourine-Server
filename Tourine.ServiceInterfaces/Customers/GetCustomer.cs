using System;
using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/{Id}", "GET")]
    public class GetCustomer : IReturn<Customer>
    {
        public Guid Id { get; set; }
    }
}
