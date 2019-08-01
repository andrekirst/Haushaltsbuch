using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces.Persistance
{
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }
}