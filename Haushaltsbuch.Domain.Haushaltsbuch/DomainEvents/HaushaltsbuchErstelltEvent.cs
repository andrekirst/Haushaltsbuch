using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.DomainEvents
{
    public class HaushaltsbuchErstelltEvent : DomainEventBase<HaushaltsbuchId>
    {
        public string Name { get; private set; }
        public Währung Währung { get; private set; }

        private HaushaltsbuchErstelltEvent()
        {
        }

        internal HaushaltsbuchErstelltEvent(string name, Währung währung, HaushaltsbuchId aggregateId)
            : base(aggregateId: aggregateId)
        {
            Name = name;
            Währung = währung;
        }

        private HaushaltsbuchErstelltEvent(HaushaltsbuchId aggregateId, long aggregateVersion, string name, Währung währung)
            : base(aggregateId: aggregateId, aggregateVersion: aggregateVersion)
        {
            Name = name;
            Währung = währung;
        }

        public override IDomainEvent<HaushaltsbuchId> WithAggregate(HaushaltsbuchId aggregateId, long aggregateVersion) =>
            new HaushaltsbuchErstelltEvent(aggregateId: aggregateId, aggregateVersion: aggregateVersion, name: Name, währung: Währung);
    }
}