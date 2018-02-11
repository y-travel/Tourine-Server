using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Teams;

namespace Tourine.ServiceInterfaces.Passengers
{
    [Route("/passengers/register")]
    public class RegisterPassenger : IReturn<Team>
    {
        public Guid TourId { get; set; }
        public Guid BuyerId { get; set; }
        public List<Guid> PassengersId { get; set; }
    }
}
