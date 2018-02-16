using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.AgencyCustomers
{
    [Route("/agencies/persons", "PUT")]
    public class UpdateCustomerToAgency : IReturn
    {
        public Guid Id { get; set; }
        public Guid AgencyId { get; set; }
        public Guid PersonId { get; set; }
    }
}
