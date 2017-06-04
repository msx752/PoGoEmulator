using System.IO;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PoGoEmulator.Assets;
using PoGoEmulator.Database;

namespace PoGoEmulator
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<FormOptions>(options =>
            {
                options.BufferBody = true;
            });
            services.AddScoped<PoGoDbContext>();
            services.AddMvc().AddWebApiConventions();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole(Configuration.GetSection("Logging"))
                .AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Admin/Error");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}");
            });

            //app.Run(req =>
            //{
            //    return req.Response.WriteAsync("");
            //});

            Asset.ValidateAssets();
            GlobalSettings.GameMaster = new GameMaster();
        }
    }
}