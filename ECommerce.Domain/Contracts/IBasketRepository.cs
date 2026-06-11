using ECommerce.Domain.Entities.Baskets;

namespace ECommerce.Domain.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string id, CancellationToken cancellationToken = default);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasketAsync(string id, CancellationToken cancellationToken = default);
    }
}
