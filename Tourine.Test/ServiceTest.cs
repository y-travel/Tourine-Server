using NUnit.Framework;
using ServiceStack;
using Tourine.Common;

namespace Tourine.Test
{
    [TestFixture]
    public abstract class ServiceTest : InMemoryDbTest
    {
        protected AppHost AppHost => GlobalFixture.AppHost;
       
        protected JsonServiceClient Client { get; private set; }

        [SetUp]
        protected override void Setup()
        {
            base.Setup();
            Client = new JsonServiceClient(GlobalFixture.BaseUri);
        }

        [TearDown]
        protected void TearDown()
        {
        }
    }
}