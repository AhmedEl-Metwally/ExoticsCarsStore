using ExoticsCarsStoreServerSide.Domain.Contracts;
using ExoticsCarsStoreServerSide.Domain.Models;
using ExoticsCarsStoreServerSide.Persistence.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ExoticsCarsStoreServerSide.Persistence.Repository
{
    public class GenericRepository<TEntity,TKey> : IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly ExoticsCarsStoreDbContext _context;

        public GenericRepository(ExoticsCarsStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsync(TKey id) => await _context.Set<TEntity>().FindAsync(id);
        public void Remove(TEntity entity) => _context.Set<TEntity>().Remove(entity);
        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);     
    }
}
