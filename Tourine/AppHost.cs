using System;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using Tourine.ServiceInterfaces;

namespace Tourine
{
    public class AppHost : AppHostBase
    {
        public Settings Settings { get; }

        public OrmLiteConnectionFactory ConnectionFactory { get; }

        public AppHost(Settings settings, OrmLiteConnectionFactory connectionFactory) : base("Tourine Services",typeof(AppService).GetAssembly())
        {
            Settings = settings;
            ConnectionFactory = connectionFactory;
        }
        static AppHost()
        {
            RegisterLicense();
        }
        private class MyNet40PclExport : NetStandardPclExport
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

        public static void RegisterLicense()
        {
            PclExport.Instance = new MyNet40PclExport();
            Licensing.RegisterLicense(string.Empty);
        }

        public override void Configure(Container container)
        {
            container.Register<IDbConnectionFactory>(ConnectionFactory);
            container.Register(Settings);

            Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });
            Plugins.Add(new AdminFeature());
            Plugins.Add(new CorsFeature());
        }
    }
}
