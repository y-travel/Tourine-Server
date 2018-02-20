using System;
using System.Data;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Telerik.JustMock;
using Tourine.Common;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyPersons;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Places;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;
using Tourine.ServiceInterfaces.Users;

namespace Tourine.Test.ServerTest
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
   