using DEMO_AzureSignalRService.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DEMO_AzureSignalRService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddMicrosoftAccount(options => 
                {
                    options.ClientId = Configuration["ClientId"];
                    options.ClientSecret = Configuration["ClientSecret"];
                });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSignalR()
                .AddAzureSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseFileServer();
            app.UseMvc();

            app.UseAzureSignalR(routes => 
            {
                routes.MapHub<ChatHub>("/chat");
            });
        }
    }
}
