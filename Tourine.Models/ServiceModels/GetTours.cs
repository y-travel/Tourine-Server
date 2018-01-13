using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.Models.ServiceModels
{

    [Route("/customer/tours", "GET")]
    public class GetTours : QueryDb<Tour>
    {

    }

}
