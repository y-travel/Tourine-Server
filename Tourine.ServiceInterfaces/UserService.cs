using ServiceStack.OrmLite;
using Tourine.Models.DatabaseModels;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.ServiceInterfaces
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
