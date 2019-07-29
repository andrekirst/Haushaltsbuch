using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using FluentTimeSpan;
using Haushaltsbuch.UI.Web.Models;
using Haushaltsbuch.UI.Web.Services;
using Haushaltsbuch.UI.Web.Services.Benutzerkonto;
using Haushaltsbuch.WebApi.Benutzerkonto.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<Benutzerkonto, Benutzerrolle>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddUserManager<UserManager<Benutzerkonto>>()
                .AddSignInManager<SignInManager<Benutzerkonto>>();

            services.Configure<IdentityOptions>(configureOptions: options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                options.Lockout.DefaultLockoutTimeSpan = 5.Minutes();
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "0123456789";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(configure: options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = 5.Minutes();
                options.LoginPath = "/Benutzerkonto/Benutzerkonto/Anmelden";
                options.AccessDeniedPath = "/Benutzerkonto/Benutzerkonto/ZugriffVerweigert";
                options.SlidingExpiration = true;
            });

            services.AddTransient<IUserStore<Benutzerkonto>, UserStore>();
            services.AddTransient<IRoleStore<Benutzerrolle>, RoleStore>();

            services.AddResponseCaching();
            services.AddResponseCompression();

            services.AddLocalization(setupAction: localizationOptions =>
            {
                localizationOptions.ResourcesPath = "Resources";
            });

            string webApiHaushaltsbuchConnectionString = Environment.GetEnvironmentVariable(variable: "WEBAPI_HAUSHALTSBUCH_CONNECTIONSTRING") ??
                                            Configuration
                                                .GetSection(key: "WebApi")
                                                .GetSection(key: "Haushaltsbuch")
                                                .GetValue<string>(key: "ConnectionString");
            string webApiBenutzerkontoConnectionString = Environment.GetEnvironmentVariable(variable: "WEBAPI_BENUTZERKONTO_CONNECTIONSTRING") ??
                                                         Configuration
                                                             .GetSection(key: "WebApi")
                                                             .GetSection(key: "Benutzerkonto")
                                                             .GetValue<string>(key: "ConnectionString");

            foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables(target: EnvironmentVariableTarget.Process))
            {
                Console.WriteLine(value: $"Env => Key: {environmentVariable.Key} - Value: {environmentVariable.Value}");
            }

            services.AddControllersWithViews()
                .AddViewLocalization(format: LanguageViewLocationExpanderFormat.SubFolder);
            services.AddRazorPages()
                .AddViewLocalization(format: LanguageViewLocationExpanderFormat.SubFolder);

            services.AddTransient<IHaushaltsbuchService, HaushaltsbuchService>();
            services.AddTransient<IEventsService, EventsService>();
            services.AddTransient<IBenutzerkontoService, BenutzerkontoService>();
            services.AddHttpClient(name: "HaushaltsbuchAPI.Haushaltsbuch", configureClient: client =>
            {
                client.BaseAddress = new Uri(uriString: webApiHaushaltsbuchConnectionString);
                client.DefaultRequestHeaders.Add(name: "Accept", value: "application/json");
                client.DefaultRequestHeaders.Add(name: "User-Agent", value: "Haushaltsbuch.UI.Web");
            });

            services.AddHttpClient(name: "HaushaltsbuchAPI.Benutzerkonto", configureClient: client =>
            {
                client.BaseAddress = new Uri(uriString: webApiBenutzerkontoConnectionString);
                client.DefaultRequestHeaders.Add(name: "Accept", value: "application/json");
                client.DefaultRequestHeaders.Add(name: "User-Agent", value: "Haushaltsbuch.UI.Web");
            });

            services.Configure<ForwardedHeadersOptions>(configureOptions: options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseForwardedHeaders();

            app.UseAuthentication();

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
