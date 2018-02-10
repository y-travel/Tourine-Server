using ServiceStack;

namespace Tourine.ServiceInterfaces.Users
{
    [Route("/user/","POST")]
    public class PostUser : IReturn<User>
    {
        public User User { get; set; }
    }
}
