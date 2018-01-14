using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customer/", "PUT")]
    public class PutCustomer : IReturn
    {
        public Customer Customer { get; set; }
    }
}