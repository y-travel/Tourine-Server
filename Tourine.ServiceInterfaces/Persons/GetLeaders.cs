using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/leaders","GET")]
    public class GetLeaders : QueryDb<Person>
    {
    }
}
