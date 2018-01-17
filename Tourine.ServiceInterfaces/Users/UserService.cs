using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Users
{
    public class UserService : AppService
    {
        public object Get(GetUser request)
        {
            var user = Db.SingleById<User>(request.Id);
            Db.LoadReferences(user);
            return user;
        }

        public void Post(PostUser postUser)
        {
            Db.Insert(postUser.User);
        }
    }
}
