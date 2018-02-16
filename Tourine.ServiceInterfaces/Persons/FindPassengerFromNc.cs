using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/{NationalCode}", "GET")]
    public class FindPassengerFromNc : IReturn<Person>
    {
        public string NationalCode { get; set; }
    }
}
