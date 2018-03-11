using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/", "POST")]
    public class AddNewPerson : IReturn<Person>
    {
        public Person Person { get; set; }
    }
}