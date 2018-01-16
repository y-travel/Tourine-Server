using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Blocks
{
    [Route("/block", "POST")]
    public class PostBlock : IReturn
    {
        public Block Block { get; set; }
    }
}
