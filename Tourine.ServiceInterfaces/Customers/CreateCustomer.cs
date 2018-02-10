using ServiceStack;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/", "POST")]
    public class CreateCustomer : IReturn<Customer>
    {
        public Customer Customer { get; set; }
    }
}
