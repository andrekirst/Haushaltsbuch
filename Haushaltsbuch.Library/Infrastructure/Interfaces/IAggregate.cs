namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface IAggregate<TAggreagteId>
        where TAggreagteId : IAggregateId
    {
        TAggreagteId Id { get; }
    }
}