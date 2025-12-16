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
                if (specifications.Criteria is not null)
                    Query = Query.Where(specifications.Criteria);

                if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Any())
                    Query = specifications.IncludeExpressions.Aggregate(Query,(CurrentQuery,IncludeExpression) => CurrentQuery.Include(IncludeExpression));

                if (specifications.OrderBy is not null)
                    Query = Query.OrderBy(specifications.OrderBy);

                if(specifications.OrderByDescending is not null)
                    Query = Query.OrderByDescending(specifications.OrderByDescending);
            }
            return Query;
        }
    }
}
