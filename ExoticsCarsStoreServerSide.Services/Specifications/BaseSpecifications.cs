using ExoticsCarsStoreServerSide.Domain.Models;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using System.Linq.Expressions;

namespace ExoticsCarsStoreServerSide.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => IncludeExpressions.Add(includeExpression);
    }
}
