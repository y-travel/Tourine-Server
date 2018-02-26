using System;
using System.Reflection;
using System.Web;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using ServiceStack.Logging.NLogger;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using StackExchange.Profiling;
using Tourine.Common;
using Tourine.ServiceInterfaces;

namespace Tourine
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            var settings = new Settings(new AppSettings());
            JsConfigurator.Init();
            RunMigrations(settings.ConnectionString);
            var connectionFactory = new OrmLiteConnectionFactory(settings.ConnectionString,
                new SqlServer2016OrmLiteDialectProvider { StringConverter = { UseUnicode = true } })
            {
                ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
            };
            var appHost = new AppHost(settings, connectionFactory, new TourineBot(new TourineBotCmdService(connectionFactory), settings.TelegramToken));
            appHost.Init();
        }

        public static void RunMigrations(string connectionString)
        {
            var migrator = new FluentMigratorRunner(connectionString);
#if !DEBUG
            migrator.ApplicationContext = "Production";
#else
            migrator.ApplicationContext = "Development";
#endif
            migrator.Migrate(Assembly.GetExecutingAssembly());
        }

        protected void Application_BeginRequest()
        {
            MiniProfiler.Start();
        }
        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
    }

}