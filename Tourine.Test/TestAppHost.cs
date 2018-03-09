using System;
using System.Data;
using System.Reflection;
using Funq;
using NUnit.Framework;
using Quartz;
using Quartz.Impl;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using ServiceStack.Text;
using ServiceStack.Web;
using Telerik.JustMock;
using Tourine.Common;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Agencies;
using Tourine.ServiceInterfaces.AgencyPersons;
using Tourine.ServiceInterfaces.Destinations;
using Tourine.ServiceInterfaces.Persons;
using Tourine.ServiceInterfaces.Places;
using Tourine.ServiceInterfaces.Services;
using Tourine.ServiceInterfaces.TeamPassengers;
using Tourine.ServiceInterfaces.Teams;
using Tourine.ServiceInterfaces.TourDetails;
using Tourine.ServiceInterfaces.Tours;
using Tourine.ServiceInterfaces.Users;
using AuthProvider = Tourine.Common.AuthProvider;
using Service = ServiceStack.Service;

namespace Tourine.Test
{
    //@TODO should be change to BasicAppHost if we could
    public class TestAppHost : AppSelfHostBase
    {
        public OrmLiteConnectionFactory ConnectionFactory { get; }

        public Type[] TablesTypes { get; set; }

        public TourineBot MockBot = Mock.Create<TourineBot>();

        public AuthSession Session { get; set; }

        public AuthProvider mockAuthProvider = Mock.Create<Tourine.Common.AuthProvider>();
        public TestAppHost() : base("test", typeof(AppService).Assembly)
        {
            TestMode = true;
            ConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
            Session = new AuthSession { Agency = Mock.Create<Agency>(), User = Mock.Create<User>() };
        }

        private class MyNet40PclExport : Net40PclExport
        {
            public override LicenseKey VerifyLicenseKeyText(string licenseKeyText)
            {
                return new LicenseKey
                {
                    Expiry = DateTime.MaxValue,
                    Hash = string.Empty,
                    Name = "Tourine",
                    Type = LicenseType.Enterprise
                };
            }
        }

        public override void Configure(Container container)
        {
            JsConfig.EmitCamelCaseNames = true;
            //
            //            SetConfig(new HostConfig
            //            {
            //                DebugMode = true,
            //                Return204NoContentForEmptyResponse = true,
            //            });
            container.Register<IDbConnectionFactory>(ConnectionFactory);
            container.Register<TourineBot>(MockBot);
            container.RegisterFactory<IAuthSession>(() => Mock.Create<AuthUserSession>());
            container.RegisterAutoWired<AgencyService>();
            container.RegisterAutoWired<AgencyPersonService>();
            container.RegisterAutoWired<DestinationService>();
            container.RegisterAutoWired<PersonService>();
            container.RegisterAutoWired<PlaceService>();
            container.RegisterAutoWired<PassengerListService>();
            container.RegisterAutoWired<TeamPersonService>();
            container.RegisterAutoWired<TeamService>();
            container.RegisterAutoWired<TourDetailService>();
            container.RegisterAutoWired<TourService>();
            container.RegisterAutoWired<UserService>();
            container.RegisterAutoWired<AutoQuery>();
            container.RegisterFactory<IRequest>(() => new MockHttpRequest());
            Plugins.Add(new AutoQueryFeature());
            Plugins.Add(new AuthFeature(() => new AuthSession { UserAuthId = Session.UserAuthId }, new IAuthProvider[] { mockAuthProvider }));
            InitDb();
        }

        public void InitDb()
        {
            TablesTypes = new[] { typeof(Tour), typeof(User), typeof(Agency), typeof(Place), typeof(PriceDetail), typeof(Destination), typeof(Currency), typeof(Person), typeof(Person), typeof(TourDetail), typeof(TeamPerson), typeof(Team), typeof(Service), typeof(AgencyPerson) };//should be fill with tables

            using (var db = ConnectionFactory.OpenDbConnection())
                db.CreateTables(false, TablesTypes);
        }

        public static void RegisterLicense()
        {
            PclExport.Instance = new MyNet40PclExport();
            Licensing.RegisterLicense(string.Empty);
        }
    }
}
