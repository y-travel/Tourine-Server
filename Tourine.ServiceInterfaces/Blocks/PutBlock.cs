using ServiceStack;

namespace Tourine.ServiceInterfaces.Blocks
{
    [Route("/block","PUT")]
    public class PutBlock : IReturn
    {
        public Block Block { get; set; }
    }
}
