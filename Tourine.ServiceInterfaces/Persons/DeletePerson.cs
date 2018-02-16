using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/{ID}", "DELETE")]
    public class DeletePerson : IReturn
    {
        public Guid Id { get; set; }
    }
}
