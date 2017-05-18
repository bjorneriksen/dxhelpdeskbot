using System;
using System.IO;
using DXHelpDeskBot.Web.Chat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using StructureMap;

namespace DXHelpDeskBot.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; }

        IServiceProvider InitializeContainer(IServiceCollection services)
        {
            var container = new Container();

            container.Configure(config =>
            {
                config.Scan(_ =>
                {
                    _.AssemblyContainingType<Startup>();
                    _.WithDefaultConventions();
                    _.RegisterConcreteTypesAgainstTheFirstInterface();
                });

                config.Populate(services);
            });

            //var something = container.GetInstance<ISomething>();
            var hubContext = container.GetInstance<IHubContext<ChatHub>>();

            return container.GetInstance<IServiceProvider>();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
            });

            // Add framework services.
            services.AddMvc();

            var provider = InitializeContainer(services);
            return provider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDefaultFiles();
            var staticFilesPath = Path.Combine(env.ContentRootPath, @"wwwroot");
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Expires", "-1");
                },
                FileProvider = new PhysicalFileProvider(staticFilesPath),
            });
            app.UseMvc();

            app.UseWebSockets();
            app.UseSignalR();
        }
    }
}
