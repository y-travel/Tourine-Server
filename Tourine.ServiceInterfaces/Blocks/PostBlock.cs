using ServiceStack;

namespace Tourine.ServiceInterfaces.Blocks
{
    [Route("/block", "POST")]
    public class PostBlock : IReturn
    {
        public Block Block { get; set; }
    }
}
