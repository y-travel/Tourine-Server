using ServiceStack;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/", "PUT")]
    public class PutCustomer : IReturn
    {
        public Customer Customer { get; set; }
    }
}