using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces
{
    public interface IRepository<TAggregate, TAggregateId>
        where TAggregate : IAggregate<TAggregateId>
        where TAggregateId : IAggregateId
    {
        Task<TAggregate> GetByIdAsync(TAggregateId id);
        Task SaveAsync(TAggregate aggregate);
    }
}