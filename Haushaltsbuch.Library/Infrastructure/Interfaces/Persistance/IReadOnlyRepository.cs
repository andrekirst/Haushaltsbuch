using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Haushaltsbuch.Library.Infrastructure.Interfaces.Persistance
{
    public interface IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(string id);
    }
}