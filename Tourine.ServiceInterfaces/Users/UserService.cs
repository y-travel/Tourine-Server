using ServiceStack;
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

        public void Put(PutUser putUser)
        {
            if (!Db.Exists<User>(new  { Id = putUser.User.Id}))
                throw HttpError.NotFound("");
            Db.Update(putUser.User);
        }
    }
}
