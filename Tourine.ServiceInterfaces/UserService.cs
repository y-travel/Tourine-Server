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
        public object Get(GetUserInfo request)
        {
            var user = Db.SingleById<User>(request.Id);
            Db.LoadReferences(user);
            var userInfo = user.ConvertTo<UserInfo>();
            return userInfo;
        }
    }
}
