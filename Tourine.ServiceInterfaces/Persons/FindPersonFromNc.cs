using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/nc/{NationalCode}", "GET")]
    public class FindPersonFromNc : IReturn<Person>
    {
        public string NationalCode { get; set; }
    }
}
