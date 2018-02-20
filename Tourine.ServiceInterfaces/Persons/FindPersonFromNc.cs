using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/{NationalCode}", "GET")]
    public class FindPersonFromNc : IReturn<Person>
    {
        public string NationalCode { get; set; }
    }
}
