using ServiceStack.OrmLite;
using Tourine.Models.DatabaseModels;

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
    }
}
