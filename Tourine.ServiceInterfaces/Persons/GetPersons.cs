using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/person", "GET")]
    public class GetPersons : QueryDb<Person>
    {
    }
}