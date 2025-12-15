using ExoticsCarsStoreServerSide.Domain.Models;
using System.Linq.Expressions;

namespace ExoticsCarsStoreServerSide.Domain.Specifications
{
    public interface ISpecifications<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity,object>>> IncludeExpressions{ get; }
    }
}
