using System;
using System.IO;
using System.Net;
using System.Reflection;
using Funq;
using Quartz;
using Quartz.Impl;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.FluentValidation;
using ServiceStack.OrmLite;
using ServiceStack.Web;
using Tourine.ServiceInterfaces;
using ValidationException = ServiceStack.FluentValidation.ValidationException;

namespace Tourine.Common
{
    public class AppHost : AppSelfHostBase
    {
        public static readonly string JwtKey = "dG&^&%##R@F534dsaHGFD#$#RFDS";
        public Settings Settings { get; }

        public OrmLiteConnectionFactory ConnectionFactory { get; }

        public AppHost(Settings settings, OrmLiteConnectionFactory connectionFactory) : base("Tourine Services", typeof(AppService).GetAssembly())
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
            container.RegisterGeneric(typeof(IValidator<>), Assembly.Load("Tourine.ServiceInterfaces"));
            container.Register(Settings);
            GlobalRequestFilters.Add(ValidationFilter);
            ConfigureQuartzJobs();
            Plugins.Add(new AutoQueryFeature { MaxLimit = 100 });
            Plugins.Add(new AdminFeature());
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new CorsFeature());
            Plugins.Add(
                new AuthFeature(() => new AuthUserSession(), new IAuthProvider[]
                {
                    new AuthProvider(container.Resolve<IDbConnectionFactory>()),
                    new JwtAuthProvider
                    {
                        RequireSecureConnection = false,
                        ExpireTokensIn = TimeSpan.FromDays(1000),
                        AuthKey = JwtKey.ToAsciiBytes(),
                        CreatePayloadFilter = (jwt, session) =>
                        {
                            jwt["user_id"] = session.UserAuthId;
                        },
                        PopulateSessionFilter = (session, jwt, request) =>
                        {
                            session.UserAuthId = jwt["user_id"];
                        },
                        PersistSession = true,
                        SessionExpiry = TimeSpan.FromDays(365)
                    }
                })
                { IncludeRegistrationService = false, IncludeAssignRoleServices = false, IncludeAuthMetadataProvider = false, HtmlRedirect = null }
            );
        }

        private void ValidationFilter(IRequest request, IResponse response, object dto)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(dto.GetType());
            var validator = (IValidator)Container.MyTryResolve(validatorType);
            var validationResult = validator?.Validate(dto);
            if (validationResult?.IsValid ?? true) return;
            var exception = new ValidationException(validationResult.Errors);
            var error = HostContext.RaiseServiceException(request, dto, exception) ?? DtoUtils.CreateErrorResponse(dto, exception);
            response.WriteToResponse(request, error);
        }

        public static void ConfigureQuartzJobs()
        {
            // construct a scheduler factory
            ISchedulerFactory schedFact = new StdSchedulerFactory();

            // get a scheduler
            var sched = schedFact.GetScheduler();
            sched.Start();
            IJobDetail job = JobBuilder.Create<Job>()
                .WithIdentity("SendJob")
                .Build();


            var trigger = TriggerBuilder.Create()
                .WithIdentity("SendTrigger")
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(15).RepeatForever())
                //.StartAt(startTime)
                .StartNow()
                .Build();

            //sched.ScheduleJob(job, cronTrigger);
        }
    }
}
