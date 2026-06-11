using ECommerce.Domain.Common;
using System.Linq.Expressions;

namespace ECommerce.Domain.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }
        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
        bool IsTrackingEnabled { get; }
    }
}
