﻿using System;
using Funq;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.Testing;
using ServiceStack.Text;
using ServiceStack.Web;
using Telerik.JustMock;
using Tourine.ServiceInterfaces;
using Tourine.ServiceInterfaces.Common;
using Tourine.ServiceInterfaces.Models;
using Tourine.ServiceInterfaces.Reports;
using AuthProvider = Tourine.Common.AuthProvider;
using Service = ServiceStack.Service;

namespace Tourine.Test.Common
{
    //@TODO should be change to BasicAppHost if we could
    public class TestAppHost : AppSelfHostBase
    {
        public OrmLiteConnectionFactory ConnectionFactory { get; }

        public Type[] TablesTypes { get; set; }

        public TourineBot MockBot = Mock.Create<TourineBot>();

        public AuthSession Session { get; set; }

        public AuthProvider MockAuthProvider = Mock.Create<Tourine.Common.AuthProvider>();
        public Agency CurrentAgency { get; set; } = new Agency();
        public User CurrentUser { get; set; } = new User();
        public TestAppHost() : base("test", typeof(AppService).Assembly)
        {
            TestMode = true;
            ConnectionFactory = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider, true);
            Session = new AuthSession { Agency = CurrentAgency, User = CurrentUser };
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
            container.Register<IDbConnectionFactory>(ConnectionFactory);
            container.Register<TourineBot>(MockBot);
            container.RegisterFactory<IAuthSession>(() => Session);
//            RegisterServicesAsAutoWired(container);
            container.RegisterFactory<IRequest>(() => new MockHttpRequest());
            Plugins.Add(new AutoQueryFeature());
            Plugins.Add(new AuthFeature(() => new AuthSession { UserAuthId = Session.UserAuthId }, new IAuthProvider[] { MockAuthProvider }));
            InitDb();
        }

        private void RegisterServicesAsAutoWired(Container container)
        {
            var serviceTypes = new[]{typeof(AgencyService),typeof(AgencyPersonService), typeof(DestinationService), typeof(PersonService), typeof(PlaceService),
                typeof(PassengerListService),typeof(ReportService), typeof(AutoQuery), typeof(UserService), typeof(TourService), typeof(TourDetailService), typeof(TeamService),};
            container.RegisterAutoWiredTypes(serviceTypes);
        }

        public void InitDb()
        {
            TablesTypes = new[] { typeof(Tour), typeof(User), typeof(Agency), typeof(Place),
                typeof(PriceDetail), typeof(Destination), typeof(Currency), typeof(Person),
                typeof(Person), typeof(TourDetail), typeof(Team),
                typeof(Service), typeof(AgencyPerson),typeof(TourOption),typeof(PassengerList) };

            using (var db = ConnectionFactory.OpenDbConnection())
            {
                db.CreateTables(false, TablesTypes);
                db.Insert(CurrentAgency);
                db.Insert(CurrentUser);
            }

        }

        public static void RegisterLicense()
        {
            PclExport.Instance = new MyNet40PclExport();
            Licensing.RegisterLicense(string.Empty);
        }
    }
}
