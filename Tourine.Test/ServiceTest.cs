using System;
using FluentAssertions;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.OrmLite;
using Tourine.Common;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyCustomers;
using Tourine.ServiceInterfaces.Customers;
using Tourine.ServiceInterfaces.Users;

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