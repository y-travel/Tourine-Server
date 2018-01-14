using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Tours
{

    [Route("/tours", "GET")]
    public class GetTours : QueryDb<Tour>
    {
    }

}
