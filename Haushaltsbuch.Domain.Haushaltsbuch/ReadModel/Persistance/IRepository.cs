﻿using System.Threading.Tasks;

namespace Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.Persistance
{
    public interface IRepository<T> : IReadOnlyRepository<T>
        where T : IReadEntity
    {
        Task InsertAsync(T entity);

        Task UpdateAsync(T entity);
    }
}