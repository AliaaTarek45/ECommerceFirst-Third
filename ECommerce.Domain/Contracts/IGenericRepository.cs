using ECommerce.Domain.Common;

namespace ECommerce.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken cancellationToken = default);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken cancellationToken = default);

    }
}
