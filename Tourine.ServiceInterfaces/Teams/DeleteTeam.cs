using System;
using ServiceStack;

namespace Tourine.ServiceInterfaces.Teams
{
    [Route("/tours/teams/{TeamId}","DELETE")]
    public class DeleteTeam
    {
        [QueryDbField(Field = "Id")]
        public Guid TeamId { get; set; }
    }
}
