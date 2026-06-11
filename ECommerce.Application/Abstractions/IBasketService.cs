using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Basket;

namespace ECommerce.Application.Abstractions
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken cancellationToken = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken cancellationToken = default);
        Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default);
    }
}
