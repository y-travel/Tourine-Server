using ServiceStack;

namespace Tourine.ServiceInterfaces.Customers
{
    [Route("/customers", "GET")]
    public class GetCustomers : QueryDb<Customer>
    {
    }
}