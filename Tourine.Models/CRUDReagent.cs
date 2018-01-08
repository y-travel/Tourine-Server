using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace Tourine.Models
{
    [Route("/customer/reagent","POST")]
    public class PostReagent:IReturn<Reagent>
    {
        public Reagent reagent { get; set; }
    }
    [Route("/customer/reagent", "PUT")]
    public class PutReagent : IReturn<Reagent>
    {
        public Reagent reagent { get; set; }
    }

    [Route("/customer/reagent", "GET")]
    public class GetReagent : QueryDb<Reagent>
    {
    }

    [Route("/customer/reagent", "DELETE")]
    public class DeleteReagent : IReturn<Reagent>
    {
        public Guid Id { get; set; }
    }
}
