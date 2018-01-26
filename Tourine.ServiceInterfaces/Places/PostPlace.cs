using ServiceStack;

namespace Tourine.ServiceInterfaces.Places
{
    [Route("/post/","POST")]
    public class PostPlace
    {
        public Place Place { get; set; }
    }
}
