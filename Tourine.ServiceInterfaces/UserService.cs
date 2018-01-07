using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.Text;
using Tourine.Models;

namespace Tourine.ServiceInterfaces
{
    public class UserService : AppService
    {
        public IAutoQueryDb AutoQuery { get; set; }

        public object Get(GetUsers query)
        {

            if (query.Id == null)
                throw HttpError.NotFound("");
            /*            var qr = AutoQuery
                            .CreateQuery(query, Request)
                            .And(x => x.Name == query.Name);
                        return AutoQuery.Execute(query, qr);*/

            var q = Db
                .From<User>()
                .Join<Role>((u, r) => r.Id == u.RoleId)
                .Where<User>(x => x.Id == query.Id);
            var qq = Db.SelectMulti<User, Role>(q).Select(x => new UserInfo { Id = x.Item1.Id });
            return qq;
        }

        public object Get(GetUserInfo request)
        {
            var user = Db.Single<User>(x => x.Id == request.Id);
            Db.LoadReferences(user);
            var userInfo = user.ConvertTo<UserInfo>();
            return userInfo;
        }
    }
}
