using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Blocks
{
    [Route("/blocks", "GET")]
    public class GetBlocks : QueryDb<Block>
    {
    }
}
