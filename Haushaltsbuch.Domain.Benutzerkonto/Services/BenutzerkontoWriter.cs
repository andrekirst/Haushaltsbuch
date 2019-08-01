using Haushaltsbuch.Domain.Benutzerkonto.DomainEvents;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haushaltsbuch.Domain.Benutzerkonto.Services
{
    public class BenutzerkontoWriter : IBenutzerkontoWriter
    {
        private IRepository<Benutzerkonto, BenutzerkontoId> BenutzerkontoRepository { get; }
        private ITransientDomainEventSubscriber Subscriber { get; }
        private IEnumerable<IDomainEventHandler<BenutzerkontoId, BenutzerkontoRegistriertEvent>> BenutzerkontoErstellEventHandlers { get; }
        private IEnumerable<IDomainEventHandler<BenutzerkontoId, EMailAdresseNormalisiertEvent>> EmailAdresseNormalisiertEventHandlers { get; }

        public BenutzerkontoWriter(
            IRepository<Benutzerkonto, BenutzerkontoId> benutzerkontoRepository,
            ITransientDomainEventSubscriber subscriber,
            IEnumerable<IDomainEventHandler<BenutzerkontoId, BenutzerkontoRegistriertEvent>> benutzerkontoErstellEventHandlers,
            IEnumerable<IDomainEventHandler<BenutzerkontoId, EMailAdresseNormalisiertEvent>> emailAdresseNormalisiertEventHandlers)
        {
            BenutzerkontoRepository = benutzerkontoRepository;
            Subscriber = subscriber;
            BenutzerkontoErstellEventHandlers = benutzerkontoErstellEventHandlers;
            EmailAdresseNormalisiertEventHandlers = emailAdresseNormalisiertEventHandlers;
        }

        public async Task Registrieren(string anmeldenummer, string email, string passwortHash, string securityStamp)
        {
            Benutzerkonto benutzerkonto = new Benutzerkonto(
                benutzerkontoId: BenutzerkontoId.NeueBenutzerkontoId(),
                anmeldenummer: anmeldenummer,
                email: email,
                passwortHash: passwortHash,
                securityStamp: securityStamp);
            Subscriber.Subscribe<BenutzerkontoRegistriertEvent>(handler: async @event => await HandleAsync(handlers: BenutzerkontoErstellEventHandlers, @event: @event));
            await BenutzerkontoRepository.SaveAsync(aggregate: benutzerkonto);
        }

        public async Task SetzeNormalisierteEMailAdresse(string benutzerkontoId, string anmeldenummer, string normalisierteEMailAdresse)
        {
            Benutzerkonto benutzerkonto = await BenutzerkontoRepository.GetByIdAsync(id: new BenutzerkontoId(id: benutzerkontoId));
            Subscriber.Subscribe<EMailAdresseNormalisiertEvent>(handler: async @event => await HandleAsync(handlers: EmailAdresseNormalisiertEventHandlers, @event: @event));
            benutzerkonto.SetzeNormalisierteEMail(normalisiertEMail: normalisierteEMailAdresse);
            await BenutzerkontoRepository.SaveAsync(aggregate: benutzerkonto);
        }

        public async Task HandleAsync<T>(IEnumerable<IDomainEventHandler<BenutzerkontoId, T>> handlers, T @event)
            where T : IDomainEvent<BenutzerkontoId>
        {
            foreach (IDomainEventHandler<BenutzerkontoId, T> handler in handlers)
            {
                await handler.HandleAsync(@event: @event);
            }
        }
    }
}