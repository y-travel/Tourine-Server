using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces.Tours;
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

        [Test]
        public void PostTourvalidator_should_not_throw_exception()
        {
            Client.Invoking(p => p.Post(new PostTour
            {
                Tour = new Tour
                {
                    StartDate = DateTime.Today
                }
            })).ShouldNotThrow<WebServiceException>();
        }

        public void PostTourvalidator_should_throw_exception()
        {
            Client.Invoking(p => p.Post(new PostTour
            {
                Tour = new Tour
                {
                    StartDate = DateTime.MinValue
                }
            })).ShouldThrow<WebServiceException>();
        }
    }
}
