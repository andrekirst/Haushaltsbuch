using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Haushaltsbuch.Library.Infrastructure.Implementations;
using MongoDB.Driver;

namespace Haushaltsbuch.Domain.Haushaltsbuch.ReadModel.Persistance
{
    public class MongoDbRepository<TReadEntity> : IRepository<TReadEntity>
        where TReadEntity : IReadEntity
    {
        private IMongoDatabase MongoDatabase { get; }

        public MongoDbRepository(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        private string CollectionName => typeof(TReadEntity).Name;
        
        public async Task<IEnumerable<TReadEntity>> FindAllAsync(Expression<Func<TReadEntity, bool>> predicate)
        {
            var cursor = await GetCurrentCollection()
                .FindAsync(filter: predicate);
            return cursor.ToEnumerable();
        }

        public Task<TReadEntity> GetByIdAsync(string id) =>
            GetCurrentCollection()
                .Find(filter: x => x.Id == id)
                .SingleAsync();

        public async Task InsertAsync(TReadEntity entity)
        {
            try
            {
                await GetCurrentCollection()
                    .InsertOneAsync(document: entity);
            }
            catch (MongoWriteException e)
            {
                throw new RepositoryException(message: $"Error inserting entity {entity.Id}", innerException: e);
            }
        }

        public async Task UpdateAsync(TReadEntity entity)
        {
            try
            {
                ReplaceOneResult result = await GetCurrentCollection()
                    .ReplaceOneAsync(filter: x => x.Id == entity.Id, replacement: entity);

                if (result.MatchedCount != 1)
                {
                    throw new RepositoryException(message: $"Missing entity {entity.Id}");
                }
            }
            catch (MongoWriteException e)
            {
                throw new RepositoryException(message: $"Error updating entity {entity.Id}", innerException: e);
            }
        }

        private IMongoCollection<TReadEntity> GetCurrentCollection() => MongoDatabase.GetCollection<TReadEntity>(name: CollectionName);
    }
}