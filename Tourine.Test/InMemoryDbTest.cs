using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using NUnit.Framework;
using ServiceStack.OrmLite;
using Tourine;
using Tourine.Models;
using Tourine.Common;

namespace Tourine.Test
{
    [TestFixture]
    public abstract class InMemoryDbTest
    {
        protected IDbConnection Db { get; }

        protected InMemoryDbTest()
        {
            Db = GlobalFixture.ConnectionFactory.OpenDbConnection();
        }

        [SetUp]
        protected virtual void Setup()
        {
            foreach (var type in GlobalFixture.TablesTypes) Db.DeleteAll(type);
        }

        protected InsertHelper<T> Insert<T>(T entity)
        {
            return new InsertHelper<T>(Db, entity);
        }
    }
    public class InsertHelper<T>
    {
        public T Result { get; }

        private IDbConnection Db { get; }

        public InsertHelper(IDbConnection db, T result)
        {
            Db = db;
            Result = result;
            Save();
        }

        public InsertHelper<T2> Insert<T2>(T2 entity)
        {
            return new InsertHelper<T2>(Db, entity);
        }

        public InsertHelper<T2> Insert<T2>(Func<T, T2> expression)
        {
            return new InsertHelper<T2>(Db, expression(Result));
        }

        protected void Save()
        {
            var autoIncrementField = Result.GetType().GetModelMetadata().FieldDefinitions.FirstOrDefault(x => x.AutoIncrement);
            Db.Insert(Result);
            autoIncrementField?.SetValueFn(Result, (int)Db.LastInsertId());
        }
    }

}

[SetUpFixture]
public class GlobalFixture
{
    public const string BaseUri = "http://localhost:2000/api/";

    public static OrmLiteConnectionFactory ConnectionFactory { get; private set; }

    public static AppHost AppHost { get; private set; }

    public static Type[] TablesTypes { get; private set; }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        JsConfigurator.Init();
        AppHost.RegisterLicense();
        ConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
        var settings = new Settings();

        AppHost = (AppHost)new AppHost(settings, ConnectionFactory) { TestMode = true }.Init().Start(BaseUri);

        TablesTypes = new[] { typeof(User) };//should be fill with tables

        using (var db = ConnectionFactory.OpenDbConnection())
            db.CreateTables(false, TablesTypes);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        AppHost.Dispose();
    }

}

