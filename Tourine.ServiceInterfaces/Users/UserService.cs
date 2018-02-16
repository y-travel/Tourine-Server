using System;
using ServiceStack;
using ServiceStack.OrmLite;

namespace Tourine.ServiceInterfaces.Users
{
    public class UserService : AppService
    {
        public TourineBot Bot { get; set; }

        [Authenticate]
        public object Get(GetUser request)
        {
            var user = Db.SingleById<User>(request.Id);
            Db.LoadReferences(user);
            user.Roles = user.Role.ParseRole<Role>();
            return user;
        }

        [Authenticate]
        public object Post(PostUser postUser)
        {
            postUser.User.Id = Guid.NewGuid();
            Db.Insert(postUser.User);
            return Db.SingleById<User>(postUser.User.Id);
        }

        [Authenticate]
        public void Put(PutUser putUser)
        {
            if (!Db.Exists<User>(new { Id = putUser.User.Id }))
                throw HttpError.NotFound("");
            Db.Update(putUser.User);
        }
    }
}
