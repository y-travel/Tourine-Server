using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test
{
    public class AuthProviderTest : ServiceTest
    {
        [Test]
        public void try_authenticate_should_return_result()
        {
            Db.Insert(new User { Username = "test", Password = "test" });
            Client.Invoking(x => x.Post(new Authenticate { UserName = "test", Password = "test" }))
                .ShouldNotThrow();
        }
    }
}
