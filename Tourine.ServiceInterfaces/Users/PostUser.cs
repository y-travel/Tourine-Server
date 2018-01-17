using ServiceStack;

namespace Tourine.ServiceInterfaces.Users
{
    [Route("/user/","POST")]
    public class PostUser : IReturn
    {
        public User User { get; set; }
    }
}
