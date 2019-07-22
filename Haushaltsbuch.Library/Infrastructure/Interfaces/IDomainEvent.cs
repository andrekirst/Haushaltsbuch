using System;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface IDomainEvent<TAggregateId>
    {
        Guid EventId { get; }

        TAggregateId AggregateId { get; }

        long AggregateVersion { get; }
    }
}