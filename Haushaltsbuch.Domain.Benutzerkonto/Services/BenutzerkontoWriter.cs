using System;
using Haushaltsbuch.Domain.Benutzerkonto.DomainEvents;
using Haushaltsbuch.Library.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Haushaltsbuch.Domain.Benutzerkonto.Services
{
    public class BenutzerkontoWriter : IBenutzerkontoWriter
    {
        private IRepository<Benutzerkonto, BenutzerkontoId> BenutzerkontoRepository { get; }
        private IDateTimeOffsetProvider DateTimeOffsetProvider { get; }
        private ITransientDomainEventSubscriber Subscriber { get; }
        private IEnumerable<IDomainEventHandler<BenutzerkontoId, BenutzerkontoRegistriertEvent>> BenutzerkontoErstellEventHandlers { get; }
        private IEnumerable<IDomainEventHandler<BenutzerkontoId, EMailAdresseNormalisiertEvent>> EmailAdresseNormalisiertEventHandlers { get; }
        private IEnumerable<IDomainEventHandler<BenutzerkontoId, BenutzerAngemeldetEvent>> BenutzerAngemeldetEventHandlers { get; }

        public BenutzerkontoWriter(
            IRepository<Benutzerkonto, BenutzerkontoId> benutzerkontoRepository,
            IDateTimeOffsetProvider dateTimeOffsetProvider,
            ITransientDomainEventSubscriber subscriber,
            IEnumerable<IDomainEventHandler<BenutzerkontoId, BenutzerkontoRegistriertEvent>> benutzerkontoErstellEventHandlers,
            IEnumerable<IDomainEventHandler<BenutzerkontoId, EMailAdresseNormalisiertEvent>> emailAdresseNormalisiertEventHandlers,
            IEnumerable<IDomainEventHandler<BenutzerkontoId, BenutzerAngemeldetEvent>> benutzerAngemeldetEventHandlers)
        {
            BenutzerkontoRepository = benutzerkontoRepository;
            DateTimeOffsetProvider = dateTimeOffsetProvider;
            Subscriber = subscriber;
            BenutzerkontoErstellEventHandlers = benutzerkontoErstellEventHandlers;
            EmailAdresseNormalisiertEventHandlers = emailAdresseNormalisiertEventHandlers;
            BenutzerAngemeldetEventHandlers = benutzerAngemeldetEventHandlers;
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

        public async Task Anmelden(string benutzerkontoId, string anmeldenummer)
        {
            Benutzerkonto benutzerkonto = await BenutzerkontoRepository.GetByIdAsync(id: new BenutzerkontoId(id: benutzerkontoId));
            Subscriber.Subscribe<BenutzerAngemeldetEvent>(handler: async @event => await HandleAsync(handlers: BenutzerAngemeldetEventHandlers, @event: @event));
            benutzerkonto.Anmelden(anmeldedatum: DateTimeOffsetProvider.Now);
            await BenutzerkontoRepository.SaveAsync(aggregate: benutzerkonto);
        }

        //private async Task Do<TAggregateId, TAggregate, TEvent>(
        //    TAggregateId aggregateId,
        //    IRepository<TAggregate, TAggregateId> repository,
        //    ITransientDomainEventSubscriber subscriber,
        //    IEnumerable<IDomainEventHandler<TAggregateId, TEvent>> handlers,
        //    Action<TAggregate> action)
        //    where TAggregateId : IAggregateId
        //    where TAggregate : IAggregate<TAggregateId>
        //    where TEvent : IDomainEvent<TAggregateId>
        //{
        //    TAggregate aggregate = await repository.GetByIdAsync(id: aggregateId);
        //    subscriber.Subscribe<DomainEventBase<TAggregateId>>(handler: async @event => await HandleAsync(handlers: handlers, @event: @event));
        //    action(obj: aggregate);
        //    await repository.SaveAsync(aggregate: aggregate);
        //}

        public async Task HandleAsync<TEvent, TAggregateId>(IEnumerable<IDomainEventHandler<TAggregateId, TEvent>> handlers, TEvent @event)
            where TAggregateId : IAggregateId
            where TEvent : IDomainEvent<TAggregateId>
        {
            foreach (IDomainEventHandler<TAggregateId, TEvent> handler in handlers)
            {
                await handler.HandleAsync(@event: @event);
            }
        }
    }
}