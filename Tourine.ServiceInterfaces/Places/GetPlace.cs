using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Places
{
    [Route("/places")]
    public class GetPlace : QueryDb<Place>
    {
    }
}
