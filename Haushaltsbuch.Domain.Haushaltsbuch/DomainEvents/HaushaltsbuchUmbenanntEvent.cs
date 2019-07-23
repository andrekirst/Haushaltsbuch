using Haushaltsbuch.Library.Infrastructure.Interfaces;

namespace Haushaltsbuch.Domain.Haushaltsbuch.DomainEvents
{
    public class HaushaltsbuchUmbenanntEvent : DomainEventBase<HaushaltsbuchId>
    {
        public string NeuerName { get; private set; }
        public string AlterName { get; private set; }

        private HaushaltsbuchUmbenanntEvent()
        {
        }

        internal HaushaltsbuchUmbenanntEvent(string neuerName, string alterName)
        {
            NeuerName = neuerName;
            AlterName = alterName;
        }

        public HaushaltsbuchUmbenanntEvent(HaushaltsbuchId haushaltsbuchId, long aggregateVersion, string neuerName, string alterName)
            : base(aggregateId: haushaltsbuchId, aggregateVersion: aggregateVersion)
        {
            NeuerName = neuerName;
            AlterName = alterName;
        }

        public override IDomainEvent<HaushaltsbuchId> WithAggregate(HaushaltsbuchId aggregateId, long aggregateVersion) =>
            new HaushaltsbuchUmbenanntEvent(
                haushaltsbuchId: aggregateId,
                aggregateVersion: aggregateVersion,
                neuerName: NeuerName,
                alterName: AlterName);
    }
}