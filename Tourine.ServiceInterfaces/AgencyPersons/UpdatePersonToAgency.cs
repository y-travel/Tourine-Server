using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.AgencyPersons
{
    [Route("/agencies/persons", "PUT")]
    public class UpdatePersonToAgency : IReturn
    {
        public Guid Id { get; set; }
        public Guid AgencyId { get; set; }
        public Guid PersonId { get; set; }
    }
}
