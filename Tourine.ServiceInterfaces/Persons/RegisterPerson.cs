using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.Persons
{
    [Route("/persons/register")]
    public class RegisterPerson : IReturn<Team>
    {
        public Guid TourId { get; set; }
        public Guid BuyerId { get; set; }
        public List<Guid> PassengersId { get; set; }
    }
}
