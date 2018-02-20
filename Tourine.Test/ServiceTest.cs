using System;
using System.Data;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Common;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.Users;
using Tourine.Test.Common;
using Tourine.Test.ServerTest;

namespace Tourine.Test
{
    [TestFixture]
    public abstract class ServiceTest<T> where T :AppService
    {
        public static TestAppHost AppHost { get; set; }
        protected JsonServiceClient Client { get; private set; }
        protected T MockService { get; set; }
        protected IDbConnection Db { get; set; }
        [SetUp]
        protected void Setup()
        {
            MockService = AppHost.Container.Resolve<T>();
            Db = AppHost.ConnectionFactory.OpenDbConnection();
        }

        [TearDown]
        protected void TearDown()
        {

            foreach (var type in AppHost.TablesTypes) Db.DeleteAll(type);
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            TestAppHost.RegisterLicense();
            AppHost = (TestAppHost) new TestAppHost().Init();
        }
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            AppHost.Dispose();
        }

        public void InsertDb<T>(T model, bool saveSession = false)
        {
            Db.Insert(model);
            if (model.GetType() == typeof(User) && saveSession)
            {
                var user = model as User;
                AppHost.Session.User = user;
                AppHost.Session.UserAuthId = user.Id.ToString();
                AppHost.Session.Roles = user.Role.ParseRole<string>();
            }
            else if (model.GetType() == typeof(Agency) && saveSession)
            {
                AppHost.Session.Agency = model as Agency;
            }

        }
    }

}