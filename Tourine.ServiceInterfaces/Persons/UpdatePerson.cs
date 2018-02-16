using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/", "PUT")]
    public class UpdatePerson : IReturn
    {
        public Person Person { get; set; }
    }
}