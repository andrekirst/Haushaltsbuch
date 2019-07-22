using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Haushaltsbuch.UI.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Haushaltsbuch.UI.Web
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
            services.Configure<CookiePolicyOptions>(configureOptions: options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
            });

            services.AddResponseCaching();
            services.AddResponseCompression();

            string webApiConnectionString = Environment.GetEnvironmentVariable("WEBAPI_CONNECTIONSTRING") ??
                                            Configuration.GetSection(key: "WebApi").GetValue<string>(key: "ConnectionString");

            foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables(target: EnvironmentVariableTarget.Process))
            {
                Console.WriteLine($"Env => Key: {environmentVariable.Key} - Value: {environmentVariable.Value}");
            }

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddTransient<IHaushaltsbuchService, HaushaltsbuchService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddHttpClient(name: "HaushaltsbuchAPI", configureClient: client =>
            {
                client.BaseAddress = new Uri(uriString: webApiConnectionString);
                client.DefaultRequestHeaders.Add(name: "Accept", value: "application/json");
                client.DefaultRequestHeaders.Add(name: "User-Agent", value: "Haushaltsbuch.UI.Web");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorHandlingPath: "/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            List<CultureInfo> cultures = new List<CultureInfo>
            {
                new CultureInfo(name: "de-DE")
            };

            app.UseRequestLocalization(options: new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "de-DE"),
                SupportedCultures = cultures,
                SupportedUICultures = cultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
