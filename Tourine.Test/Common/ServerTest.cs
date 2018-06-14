using System.Data;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Telerik.JustMock;
using Tourine.Common;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Common;

namespace Tourine.Test.Common
{
    [TestFixture]
    public abstract class ServerTest
    {
        public const string BaseUri = "http://localhost:2000/api/";
        protected IDbConnection Db { get; set; }
        protected JsonServiceClient Client { get; private set; }
        public static ServiceStackHost AppHost { get; set; }

        public OrmLiteConnectionFactory ConnectionFactory { get; set; }

        protected ServerTest()
        {
//            Client = new JsonServiceClient(BaseUri);
        }

        [OneTimeSetUp]
        protected virtual void Setup()
        {
            JsConfigurator.Init();
            ConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            AppHost = new AppHost(new Settings(), ConnectionFactory, Mock.Create<TourineBot>()).Init().Start(BaseUri);
            Db = AppHost.GetDbConnection();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            AppHost.Dispose();
        }

        public virtual void CreateAppHostFactory()
        {
        }

        protected InsertHelper<T> Insert<T>(T entity)
        {
            return new InsertHelper<T>(Db, entity);
        }
    }
}
   