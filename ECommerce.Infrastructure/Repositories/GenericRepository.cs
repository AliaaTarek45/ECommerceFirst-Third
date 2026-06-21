using ECommerce.Domain.Common;
using ECommerce.Domain.Contracts;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => dbContext.Set<TEntity>().AddAsync(entity, cancellationToken).AsTask();

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);
        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
            => dbContext.Set<TEntity>().FindAsync([id!], cancellationToken).AsTask();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken cancellationToken = default)
           => await SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync(cancellationToken);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken cancellationToken = default)
            => await SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specifications).ToListAsync(cancellationToken);

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken cancellationToken = default)
            => await SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specifications).CountAsync(cancellationToken);
    }
}
