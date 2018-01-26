using ServiceStack;

namespace Tourine.ServiceInterfaces.Users
{
    [Route("/user","PUT")]
    public class PutUser : IReturn
    {
        public User User { get; set; }
    }
}
