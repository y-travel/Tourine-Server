using System;
using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.Models.ServiceModels
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
