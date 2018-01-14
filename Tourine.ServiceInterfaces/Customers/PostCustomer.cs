using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/", "POST")]
    public class PostCustomer : IReturn
    {
        public Customer Customer { get; set; }
    }
}
