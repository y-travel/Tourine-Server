using System;
using System.Collections.Generic;
using System.Linq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Web;
using Tourine.ServiceInterfaces.Customers;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Common
{
    public class AuthProvider : CredentialsAuthProvider
    {
        private IDbConnectionFactory ConnectionFactory { get; }

        private LoginInfo LoginInfo { get; set; }

        public AuthProvider(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public override bool TryAuthenticate(IServiceBase authService, string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return false;
            using (var db = ConnectionFactory.OpenDbConnection())
                LoginInfo = db.Single<User>(x => x.Username == userName && x.Password == password)
                    .ConvertTo<LoginInfo>();
            return LoginInfo != null;
        }

        public override IHttpResult OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            session.UserAuthId = LoginInfo.Id.ToString();
            session.DisplayName = $"{LoginInfo.Name} {LoginInfo.Family}";
            return base.OnAuthenticated(authService, session, tokens, authInfo);
        }
    }

    public class LoginInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Family { get; set; }

        public string MobileNumber { get; set; }

        public string Password { get; set; }

        public string[] Roles { get; set; }

        public bool IsLaboratory { get; set; }
    }

}