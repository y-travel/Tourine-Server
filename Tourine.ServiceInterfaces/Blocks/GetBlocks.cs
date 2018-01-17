using ServiceStack;

namespace Tourine.ServiceInterfaces.Blocks
{
    [Route("/blocks", "GET")]
    public class GetBlocks : QueryDb<Block>
    {
    }
}
