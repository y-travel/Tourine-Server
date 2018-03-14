using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/", "PUT")]
    public class UpdatePerson : IReturn<Person>
    {
        public Person Person { get; set; }
    }
}