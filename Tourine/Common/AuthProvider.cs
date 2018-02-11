using System.Collections.Generic;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Web;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Common
{
    public class AuthProvider : CredentialsAuthProvider
    {
        private IDbConnectionFactory ConnectionFactory { get; }

        private User User { get; set; }

        public AuthProvider(IDbConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory;
        }

        public override bool TryAuthenticate(IServiceBase authService, string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return false;
            using (var db = ConnectionFactory.OpenDbConnection())
            {
                User = db.Single<User>(x => x.Username == userName && x.Password == password);
                if(User != null) db.LoadReferences(User);
            }
            return User != null;
        }

        public override IHttpResult OnAuthenticated(IServiceBase authService, IAuthSession session, IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            session.UserAuthId = User.Id.ToString();
            session.DisplayName = $"{User.Customer.Name} {User.Customer.Family}";
            return base.OnAuthenticated(authService, session, tokens, authInfo);
        }
    }
}