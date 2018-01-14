using System.Reflection;
using System.Web;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using Tourine.Common;

namespace Tourine
{
    public class Application : HttpApplication
    {
        protected void Application_Start()
        {
            var settings = new Settings(new AppSettings());
            JsConfigurator.Init();
            RunMigrations(settings.ConnectionString);

            var appHost = new AppHost(settings, new OrmLiteConnectionFactory(settings.ConnectionString,
                new SqlServer2016OrmLiteDialectProvider {StringConverter = {UseUnicode = true}}));
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
    }

}