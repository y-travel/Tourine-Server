using System;
using System.Collections.Generic;
using ServiceStack;
using Tourine.ServiceInterfaces.Persons;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/tours/{TourId}/teams/{TeamId}", "POST")]
    public class UpsertTeam : IReturn<Team>
    {
        public Guid? TeamId { get; set; }
        public Guid TourId { get; set; }
        public Person Buyer { get; set; }
        public List<TeamMember> Passengers { get; set; }
        public long InfantPrice { get; set; }
        public long BasePrice { get; set; }
        public long TotalPrice { get; set; }
    }
}
 