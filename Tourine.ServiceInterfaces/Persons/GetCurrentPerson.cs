using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/current","GET")]
    public class GetCurrentPerson : IReturn<Person>
    {
    }
}
