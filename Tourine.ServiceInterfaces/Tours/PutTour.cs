using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tour/", "PUT")]
    public class PutTour : IReturn
    {
        public Tour Tour { get; set; }
    }
}
