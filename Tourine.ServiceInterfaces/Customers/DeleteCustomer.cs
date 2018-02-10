using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/{ID}", "DELETE")]
    public class DeleteCustomer : IReturn
    {
        public Guid Id { get; set; }
    }
}