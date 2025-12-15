using ExoticsCarsStoreServerSide.Domain.Specifications;
using ExoticsCarsStoreServerSide.Domain.Models;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;

namespace ExoticsCarsStoreServerSide.Persistence.Repository
{
    public class UnitOfWork(ExoticsCarsStoreDbContext _context) : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repository = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var EntityType = typeof(TEntity);
            if (_repository.TryGetValue(EntityType, out object? repository))
                return (IGenericRepository<TEntity, TKey>)repository;

            var Repository = new GenericRepository<TEntity,TKey>(_context);
            _repository[EntityType] = Repository;
            return Repository;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
     
    }
}
