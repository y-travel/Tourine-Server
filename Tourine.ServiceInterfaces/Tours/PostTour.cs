using ServiceStack;
using Tourine.Models.DatabaseModels;

namespace Tourine.ServiceInterfaces.Tours
{
    [Route("/tour/", "POST")]
    public class PostTour : IReturn
    {
        public Tour Tour { get; set; }
    }
}
