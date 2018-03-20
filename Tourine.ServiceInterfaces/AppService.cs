using ServiceStack;

namespace Tourine.ServiceInterfaces
{
    public abstract class AppService : Service
    {
        private AuthSession _session;
        public AuthSession Session
        {
            get => _session ??
                (_session = ServiceStackHost.Instance.TestMode ?
                Request.SessionAs<AuthSession>() :
                Request.GetAuthSession(Db));
            set => _session = value;
        }
    }
}
