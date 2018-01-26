using ServiceStack;

namespace Tourine.ServiceInterfaces.Places
{
    [Route("/place", "PUT")]
    public class PutPlace
    {
        public Place Place { get; set; }
    }
}
