using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/{Id}", "DELETE")]
    public class DeleteCustomer : IReturn
    {
        public Guid Id { get; set; }
    }
}