using System;
using System.Collections;
using EventStore.ClientAPI;
using FluentTimeSpan;
using Haushaltsbuch.Library.Domain;
using Haushaltsbuch.Library.Domain.DomainEvents;
using Haushaltsbuch.Library.Domain.EventHandlers;
using Haushaltsbuch.Library.Domain.Queries;
using Haushaltsbuch.Library.Domain.ReadModel.Persistance;
using Haushaltsbuch.Library.Domain.Services;
using Haushaltsbuch.Library.Infrastructure;
using Haushaltsbuch.Library.Infrastructure.Implementations;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

namespace Haushaltsbuch.WebApi
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
            string eventStoreConnectionString = Environment.GetEnvironmentVariable(variable: "EVENTSTORE_CONNECTIONSTRING") ??
                                                Configuration.GetSection(key: "EventStore").GetValue<string>(key: "ConnectionString");
            string mongoDbConnectionString = Environment.GetEnvironmentVariable(variable: "MONGODB_CONNECTIONSTRING") ??
                                             Configuration.GetSection(key: "MongoDB").GetValue<string>(key: "ConnectionString");

            foreach (DictionaryEntry environmentVariable in Environment.GetEnvironmentVariables(target: EnvironmentVariableTarget.Process))
            {
                Console.WriteLine(value: $"Env => Key: {environmentVariable.Key} - Value: {environmentVariable.Value}");
            }

            services.AddControllers();
            services.AddCors(setupAction: o =>
                o.AddPolicy(name: "MyPolicy", configurePolicy: builder => { builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }));

            services.AddTransient<IEventStoreConnection>(implementationFactory: factory =>
            {
                ConnectionSettings settings = ConnectionSettings
                    .Create()
                    .KeepReconnecting()
                    .SetQueueTimeoutTo(5.Seconds())
                    .UseConsoleLogger()
                    .Build();
                IEventStoreConnection connection = EventStoreConnection.Create(
                    uri: new Uri(uriString: eventStoreConnectionString),
                    connectionName: "Haushaltsbuch",
                    connectionSettings: settings);
                
                return connection;
            });
            services.AddTransient<ITransientDomainEventPublisher, TransientDomainEventPublisherSubscriber>();
            services.AddTransient<ITransientDomainEventSubscriber, TransientDomainEventPublisherSubscriber>();
            services.AddTransient<
                IRepository<Library.Domain.Haushaltsbuch, HaushaltsbuchId>,
                EventSourcingRepository<Library.Domain.Haushaltsbuch, HaushaltsbuchId>>();
            services.AddSingleton<IEventStore, Library.Infrastructure.Implementations.EventStore>();
            services.AddSingleton<IMongoClient>(implementationFactory: factory => new MongoClient(connectionString: mongoDbConnectionString));
            services.AddSingleton<IMongoDatabase>(implementationFactory: factory => factory.GetRequiredService<IMongoClient>().GetDatabase(name: "HaushaltsbuchReadModel"));
            services.AddTransient<
                IReadOnlyRepository<Library.Domain.ReadModel.Haushaltsbuch>,
                MongoDbRepository<Library.Domain.ReadModel.Haushaltsbuch>>();
            services.AddTransient<
                IRepository<Library.Domain.ReadModel.Haushaltsbuch>,
                MongoDbRepository<Library.Domain.ReadModel.Haushaltsbuch>>();
            services.AddTransient<
                IReadOnlyRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>,
                MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>>();
            services.AddTransient<
                IRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>,
                MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>>();
            services.AddTransient<
                IReadOnlyRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>,
                MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>>();
            services.AddTransient<
                IRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>,
                MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>>();
            services.AddTransient<IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchErstelltEvent>, HaushaltsbuchEventHandler>();
            services.AddTransient<IDomainEventHandler<HaushaltsbuchId, InHaushaltsbuchEingezahltEvent>, HaushaltsbuchEventHandler>();
            services.AddTransient<IDomainEventHandler<HaushaltsbuchId, AusHaltshaltsbuchAusgezahltEvent>, HaushaltsbuchEventHandler>();
            services.AddTransient<IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchUmbenanntEvent>, HaushaltsbuchEventHandler>();
            services.AddTransient<IHaushaltsbuchWriter, HaushaltsbuchWriter>();
            services.AddTransient<IHaushaltsbuchReader, HaushaltsbuchReader>();
            services.AddSingleton<IUebersichtQueries, UebersichtQueries>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseCors(policyName: "MyPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(configure: endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
