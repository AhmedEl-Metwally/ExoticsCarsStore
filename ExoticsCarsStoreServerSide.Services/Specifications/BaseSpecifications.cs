using ExoticsCarsStoreServerSide.Domain.Models;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using System.Linq.Expressions;

namespace ExoticsCarsStoreServerSide.Services.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) => IncludeExpressions.Add(includeExpression);

        public Expression<Func<TEntity, bool>> Criteria { get; }
        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpressions) => Criteria = CriteriaExpressions;

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression) => OrderBy = orderByExpression;

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression) => OrderByDescending = orderByDescendingExpression;  
    }
}
