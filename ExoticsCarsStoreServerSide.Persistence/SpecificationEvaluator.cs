using ExoticsCarsStoreServerSide.Domain.Models;
using ExoticsCarsStoreServerSide.Domain.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ExoticsCarsStoreServerSide.Persistence
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryPoint,ISpecifications<TEntity,TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = EntryPoint;
            if (specifications is not null)
            {
                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                    Query = specifications.IncludeExpressions.Aggregate(Query,(CurrentQuery,IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            }
            return Query;
        }
    }
}
