using System;
using System.Collections.Generic;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public abstract class DomainEventBase<TAggregateId>
        : IDomainEvent<TAggregateId>, IEquatable<DomainEventBase<TAggregateId>>
    {
        protected DomainEventBase()
        {
            EventId = Guid.NewGuid();
            Created = DateTime.Now;
        }

        protected DomainEventBase(TAggregateId aggregateId)
            : this()
        {
            AggregateId = aggregateId;
        }

        protected DomainEventBase(TAggregateId aggregateId, long aggregateVersion)
            : this(aggregateId: aggregateId)
        {
            AggregateVersion = aggregateVersion;
        }

        public Guid EventId { get; private set; }
        public DateTime Created { get; private set; }
        public TAggregateId AggregateId { get; private set; }
        public long AggregateVersion { get; private set; }

        public override bool Equals(object obj) => base.Equals(obj: obj as DomainEventBase<TAggregateId>);

        public bool Equals(DomainEventBase<TAggregateId> other) => other != null && EventId.Equals(g: other.EventId);

        public override int GetHashCode() => 290933282 + EqualityComparer<Guid>.Default.GetHashCode(obj: EventId);

        public abstract IDomainEvent<TAggregateId> WithAggregate(TAggregateId aggregateId, long aggregateVersion);
    }
}