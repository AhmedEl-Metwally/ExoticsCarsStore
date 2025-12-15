using ExoticsCarsStoreServerSide.Domain.Models;

namespace ExoticsCarsStoreServerSide.Domain.Specifications
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity,TKey> GetRepository<TEntity,TKey>() where TEntity : BaseEntity<TKey>;
    }
}
