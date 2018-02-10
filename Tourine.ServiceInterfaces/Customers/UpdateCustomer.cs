using ServiceStack;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/", "PUT")]
    public class UpdateCustomer : IReturn
    {
        public Customer Customer { get; set; }
    }
}