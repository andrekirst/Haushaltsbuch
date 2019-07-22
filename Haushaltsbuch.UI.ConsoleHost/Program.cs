using System.IO.Abstractions;
using System.Threading.Tasks;
using Autofac;
using Haushaltsbuch.Library.Domain;
using Haushaltsbuch.Library.Domain.Commandline;
using Haushaltsbuch.Library.Domain.DomainEvents;
using Haushaltsbuch.Library.Domain.EventHandlers;
using Haushaltsbuch.Library.Domain.Queries;
using Haushaltsbuch.Library.Domain.ReadModel.Persistance;
using Haushaltsbuch.Library.Domain.Services;
using Haushaltsbuch.Library.Infrastructure;
using Haushaltsbuch.Library.Infrastructure.Implementations;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using Haushaltsbuch.Library.Interactors;
using MongoDB.Driver;
using FileSystem = System.IO.Abstractions.FileSystem;

namespace Haushaltsbuch.UI.ConsoleHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<FileEventStore>()
                .As<IEventStore>()
                .SingleInstance();
            builder.RegisterType<TransientDomainEventPublisherSubscriber>()
                .As<ITransientDomainEventSubscriber>()
                .As<ITransientDomainEventPublisher>();
            builder.RegisterType<EventSourcingRepository<Library.Domain.Haushaltsbuch, HaushaltsbuchId>>()
                .As<IRepository<Library.Domain.Haushaltsbuch, HaushaltsbuchId>>();
            builder.RegisterType<FileEventStore>()
                .As<IEventStore>()
                .SingleInstance();
            builder.Register(@delegate: x => new MongoClient(connectionString: "mongodb://localhost:27017"))
                .As<IMongoClient>()
                .SingleInstance();
            builder.Register(@delegate: x => x.Resolve<IMongoClient>().GetDatabase(name: "HaushaltsbuchReadModel"))
                .As<IMongoDatabase>()
                .SingleInstance();
            builder.RegisterType<MongoDbRepository<Library.Domain.ReadModel.Haushaltsbuch>>()
                .As<IReadOnlyRepository<Library.Domain.ReadModel.Haushaltsbuch>>();
            builder.RegisterType<MongoDbRepository<Library.Domain.ReadModel.Haushaltsbuch>>()
                .As<IRepository<Library.Domain.ReadModel.Haushaltsbuch>>();
            builder.RegisterType<MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>>()
                .As<IReadOnlyRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>>();
            builder.RegisterType<MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>>()
                .As<IRepository<Library.Domain.ReadModel.HaushaltsbuchEinzahlung>>();
            builder.RegisterType<MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>>()
                .As<IReadOnlyRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>>();
            builder.RegisterType<MongoDbRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>>()
                .As<IRepository<Library.Domain.ReadModel.HaushaltsbuchAuszahlung>>();
            builder.RegisterType<HaushaltsbuchEventHandler>()
                .As<IDomainEventHandler<HaushaltsbuchId, HaushaltsbuchErstelltEvent>>()
                .As<IDomainEventHandler<HaushaltsbuchId, InHaushaltsbuchEingezahltEvent>>()
                .As<IDomainEventHandler<HaushaltsbuchId, AusHaltshaltsbuchAusgezahltEvent>>();
            builder.RegisterType<HaushaltsbuchWriter>()
                .As<IHaushaltsbuchWriter>();
            builder.RegisterType<HaushaltsbuchReader>()
                .As<IHaushaltsbuchReader>();
            builder.RegisterType<FileSystem>()
                .As<IFileSystem>();
            builder.RegisterType<CommandlineParser>()
                .As<ICommandlineParser>()
                .SingleInstance();
            builder.RegisterType<MainInteractor>()
                .As<IMainInteractor>()
                .SingleInstance();
            builder.RegisterType<UebersichtQueries>()
                .As<IUebersichtQueries>()
                .SingleInstance();

            IContainer container = builder.Build();

            IMainInteractor mainInteractor = container.Resolve<IMainInteractor>();

            await mainInteractor.ExecuteAsync(args: args);
        }
    }
}
