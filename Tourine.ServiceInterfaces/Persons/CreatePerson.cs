using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/", "POST")]
    public class CreatePerson : IReturn
    {
        public Person Person { get; set; }
    }
}