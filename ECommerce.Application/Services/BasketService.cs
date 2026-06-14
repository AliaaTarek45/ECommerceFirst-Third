using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Baskets;

namespace ECommerce.Application.Services
{
    internal class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken cancellationToken = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var saved = await basketRepository.CreateOrUpdateBasketAsync(customerBasket, cancellationToken: cancellationToken)
                ?? throw new InvalidOperationException("Could not save basket. Try again later.");
            return mapper.Map<BasketDto>(saved);
        }

        public async Task<Result<bool>> DeleteBasketAsync(string id, CancellationToken cancellationToken = default)
            => await basketRepository.DeleteBasketAsync(id, cancellationToken);

        public async Task<Result<BasketDto>> GetBasketAsync(string id, CancellationToken cancellationToken = default)
        {
            var basket = await basketRepository.GetBasketAsync(id, cancellationToken);
            if (basket == null)
                return Result<BasketDto>.Fail(Error.NotFound("Basket.NotFound", $"Basket {id} Not Found"));
            return mapper.Map<BasketDto>(basket);
        }
    }
}
