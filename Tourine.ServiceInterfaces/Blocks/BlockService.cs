using ServiceStack;
using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Blocks
{
    public class BlockService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetBlocks getBlocks)
        {
            var items = AutoQuery.CreateQuery(getBlocks, Request.GetRequestParams());
            return AutoQuery.Execute(getBlocks, items);
        }

        public void Post(PostBlock postBlock)
        {
            Db.Insert(postBlock.Block);
        }
    }
}
