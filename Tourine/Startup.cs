using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;

namespace Tourine
{
    public class Startup
    {
        public Settings Settings { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("config.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Settings = new Settings(builder.Build());
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Settings.Instance.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();
                app.UseDeveloperExceptionPage();
            }
            SqliteDialect.Provider.GetStringConverter().UseUnicode = true;
            app.UseServiceStack(new AppHost(Settings, new OrmLiteConnectionFactory(Settings.ConnectionString, SqliteDialect.Provider)));
        }
    }
}
