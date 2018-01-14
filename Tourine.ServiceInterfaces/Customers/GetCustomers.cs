using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customers", "GET")]
    public class GetCustomers : QueryDb<Customer>
    {
    }
}