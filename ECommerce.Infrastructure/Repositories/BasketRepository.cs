using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Baskets;
using StackExchange.Redis;
using System.Text.Json;

namespace ECommerce.Infrastructure.Repositories
{
    internal class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string id, CancellationToken cancellationToken = default)
        {
            var basket = await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty
                ? null
                : JsonSerializer.Deserialize<CustomerBasket>(basket!);
        }

        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(basket);
            var success = await _database.StringSetAsync(basket.Id, json, timeToLive ?? TimeSpan.FromDays(30));
            return success ? basket : null; // no need to round-trip - we have the object
        }

        public async Task<bool> DeleteBasketAsync(string id, CancellationToken cancellationToken = default)
            => await _database.KeyDeleteAsync(id);
    }

}
