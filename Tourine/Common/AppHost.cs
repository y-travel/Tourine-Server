using System;
using System.IO;
using System.Net;
using Funq;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Data;
using ServiceStack.FluentValidation;
using ServiceStack.OrmLite;
using ServiceStack.Razor;
using Tourine.ServiceInterfaces;

namespace Tourine.Common
{
    public class AppHost : AppSelfHostBase
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

        public static void RegisterLicense()
        {
            PclExport.Instance = new MyNet40PclExport();
            Licensing.RegisterLicense(string.Empty);
        }

        public override void Configure(Container container)
        {
            CustomErrorHttpHandlers[HttpStatusCode.NotFound] = new CustomStaticFileHandler("/404.html");
            SetConfig(new HostConfig
            {
                HandlerFactoryPath = "api",
                ReturnsInnerException = true,
                WebHostPhysicalPath = Path.GetFullPath(Path.Combine("~".MapServerPath(), "..")),
                MapExceptionToStatusCode = { { typeof(ValidationException), 422 } },
                AdminAuthSecret = "123456"
            });
            container.Register<IDbConnectionFactory>(ConnectionFactory);
            container.Register(Settings);

            Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });
            Plugins.Add(new AdminFeature());
            Plugins.Add(new CorsFeature());
        }
    }
}
