using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Haushaltsbuch.UI.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
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

            services.AddLocalization(setupAction: localizationOptions =>
            {
                localizationOptions.ResourcesPath = "Resources";
            });

            string webApiConnectionString = Environment.GetEnvironmentVariable(variable: "WEBAPI_HAUSHALTSBUCH_CONNECTIONSTRING") ??
                                            Configuration
                                                .GetSection(key: "WebApi")
                                                .GetSection(key: "Haushaltsbuch")
                                                .GetValue<string>(key: "ConnectionString");

            foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables(target: EnvironmentVariableTarget.Process))
            {
                Console.WriteLine(value: $"Env => Key: {environmentVariable.Key} - Value: {environmentVariable.Value}");
            }

            services.AddControllersWithViews()
                .AddViewLocalization(format: LanguageViewLocationExpanderFormat.SubFolder);
            services.AddRazorPages()
                .AddViewLocalization(format: LanguageViewLocationExpanderFormat.SubFolder);
            services
                .AddAuthentication()
                .AddMicrosoftAccount(configureOptions: microsoftOptions =>
                {
                    string clientid = Environment.GetEnvironmentVariable(variable: "AUTHENTICATION_MICROSOFT_CLIENTID") ??
                                      Configuration[key: "Authentication:Microsoft:ClientId"];
                    microsoftOptions.ClientId = clientid;

                    string clientSecret = Environment.GetEnvironmentVariable(variable: "AUTHENTICATION_MICROSOFT_CLIENTSECRET") ??
                                          Configuration[key: "Authentication:Microsoft:ClientSecret"];
                    microsoftOptions.ClientSecret = clientSecret;
                });

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
                new CultureInfo(name: "de-DE"),
                new CultureInfo(name: "en-US")
            };

            app.UseRequestLocalization(options: new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "de-DE"),
                SupportedCultures = cultures,
                SupportedUICultures = cultures,
                RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new QueryStringRequestCultureProvider(),
                    new AcceptLanguageHeaderRequestCultureProvider(),
                    new CookieRequestCultureProvider()
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "HaushaltsbuchArea",
                    areaName: "Haushaltsbuch",
                    pattern: "Haushaltsbuch/{controller=Haushaltsbuch}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "BenutzerkontoArea",
                    areaName: "Benutzerkonto",
                    pattern: "Benutzerkonto/{controller=Benutzerkonto}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
