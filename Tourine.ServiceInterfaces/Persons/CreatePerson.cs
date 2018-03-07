using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/", "POST")]
    public class CreatePerson : IReturn<Person>
    {
        public Person Person { get; set; }
    }
}