using ServiceStack;

namespace Tourine.Models.DatabaseModels
{
    [Route("/customer/tour/","POST")]
    public class PostTour:IReturn<Tour>
    {
        public Tour Tour { get; set; }
    }
}
