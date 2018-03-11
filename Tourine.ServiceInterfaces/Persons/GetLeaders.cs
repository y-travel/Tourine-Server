using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/leaders","GET")]
    public class GetLeaders : QueryDb<Person>
    {
    }
}
