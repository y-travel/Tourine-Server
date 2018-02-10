using System;
using System.Data;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.OrmLite;
using ServiceStack.Web;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyCustomers;
using Tourine.ServiceInterfaces.Customers;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.ServiceInterfaces
{
    public class AuthSession : AuthUserSession
    {
        public User User { get; set; }
        public Agency Agency { get; set; }

        public bool TestMode { get; set; }

        public void SetTestMode()
        {
            TestMode = true;
        }

        public override bool IsAuthorized(string provider)
        {
            return TestMode || base.IsAuthorized(provider);
        }

        public override bool HasRole(string role, IAuthRepository authRepo)
        {
            return Roles?.Contains(role) ?? false;
            //return base.HasRole(role, authRepo);
        }
    }

    public static class SessionExtensions
    {
        public static AuthSession GetAuthSession(this IRequest request, IDbConnection db)
        {
            var session = request.SessionAs<AuthSession>();
            Guid id;
            id = Guid.TryParse(session.UserAuthId, out id) ? id : Guid.Empty;
            session.User = session.User ?? db.SingleById<User>(id);
            var agencyCustomer = db.Single<AgencyCustomer>(x => x.CustomerId == session.User.CustomerId);
            var customer = db.SingleById<Customer>(agencyCustomer.CustomerId);
            session.DisplayName = customer.Name + " " + customer.Family;
            session.Agency = session.Agency ?? db.SingleById<Agency>(agencyCustomer.AgencyId);
            return session;
        }
    }
}
