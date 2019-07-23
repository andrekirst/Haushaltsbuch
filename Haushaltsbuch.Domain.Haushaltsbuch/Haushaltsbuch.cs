using System;
using System.Collections.Generic;
using EnsureThat;
using Haushaltsbuch.Domain.Haushaltsbuch.DomainEvents;
using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch
{
    public class Haushaltsbuch : AggregateBase<HaushaltsbuchId>,
        IApplyEvent<HaushaltsbuchErstelltEvent>,
        IApplyEvent<InHaushaltsbuchEingezahltEvent>,
        IApplyEvent<AusHaltshaltsbuchAusgezahltEvent>,
        IApplyEvent<HaushaltsbuchUmbenanntEvent>
    {
        private readonly List<HaushaltsbuchEinzahlung> _einzahlungen = new List<HaushaltsbuchEinzahlung>();
        private readonly List<HaushaltsbuchAuszahlung> _auszahlungen = new List<HaushaltsbuchAuszahlung>();

        private Haushaltsbuch()
        {}

        public Haushaltsbuch(HaushaltsbuchId haushaltsbuchId, string name, Währung währung)
        {
            RaiseEvent(@event: new HaushaltsbuchErstelltEvent(aggregateId: haushaltsbuchId, name: name, währung: währung));
        }

        public double Kassenbestand { get; private set; }
        public string Name { get; private set; }
        public Währung Währung { get; private set; }
        public IReadOnlyCollection<HaushaltsbuchEinzahlung> Einzahlungen => _einzahlungen.AsReadOnly();
        public IReadOnlyCollection<HaushaltsbuchAuszahlung> Auszahlungen => _auszahlungen.AsReadOnly();

        public void Umbenennen(string neuerName)
        {
            RaiseEventIf(
                @event: new HaushaltsbuchUmbenanntEvent(neuerName: neuerName, alterName: Name),
                expression: Name != neuerName);
        }

        public void Einzahlen(double betrag, DateTimeOffset? einzahlungsDatum)
        {
            Ensure
                .That(value: betrag, name: nameof(betrag))
                .IsGt(limit: 0);

            RaiseEvent(@event: new InHaushaltsbuchEingezahltEvent(
                betrag: new Buchungsbetrag(betrag: betrag, währung: Währung),
                einzahlungsDatum: new Buchungsdatum(datum: einzahlungsDatum.GetValueOrDefault(defaultValue: DateTimeOffset.UtcNow))));
        }

        public void Auszahlen(double betrag, DateTimeOffset? auszahlungsDatum, Kategorie kategorie, string memotext)
        {
            Ensure
                .That(value: betrag, name: nameof(betrag))
                .IsGt(limit: 0);

            RaiseEvent(@event: new AusHaltshaltsbuchAusgezahltEvent(
                betrag: new Buchungsbetrag(betrag: betrag, währung: Währung),
                auszahlungsDatum: new Buchungsdatum(datum: auszahlungsDatum.GetValueOrDefault(defaultValue: DateTimeOffset.UtcNow)),
                kategorie: kategorie,
                memotext: memotext));
        }

        public void ApplyEvent(HaushaltsbuchErstelltEvent @event)
        {
            Ensure
                .That(value: @event.Name, name: nameof(@event.Name))
                .IsNotNullOrWhiteSpace();
            Id = @event.AggregateId;
            Kassenbestand = 0;
            Name = @event.Name;
            Währung = @event.Währung;
        }

        public void ApplyEvent(InHaushaltsbuchEingezahltEvent @event)
        {
            Kassenbestand += @event.Buchungsbetrag;
            _einzahlungen.Add(item: new HaushaltsbuchEinzahlung(
                einzahlungsId: Guid.NewGuid(),
                betrag: @event.Buchungsbetrag,
                einzahlungsdatum: @event.EinzahlungsDatum));
        }

        public void ApplyEvent(AusHaltshaltsbuchAusgezahltEvent @event)
        {
            Kassenbestand -= @event.Buchungsbetrag;
            _auszahlungen.Add(item: new HaushaltsbuchAuszahlung(
                auszahlungsId: Guid.NewGuid(),
                betrag: @event.Buchungsbetrag,
                auszahlungsdatum: @event.AuszahlungsDatum,
                kategorie: @event.Kategorie,
                memotext: @event.Memotext));
        }

        public void ApplyEvent(HaushaltsbuchUmbenanntEvent @event)
        {
            Ensure
                .That(value: @event.NeuerName, name: nameof(@event.NeuerName))
                .IsNotNullOrWhiteSpace();
            Name = @event.NeuerName;
        }
    }
}