using ECommerce.Application.Abstractions;
using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Basket;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    public class BasketsController(IBasketService basketService) : ApiBaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Result<BasketDto>>> GetBasket(string id, CancellationToken cancellationToken)
        {
            var basket = await basketService.GetBasketAsync(id, cancellationToken);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<Result<BasketDto>>> CreateOrUpdateBasket(BasketDto basket, CancellationToken cancellationToken)
        {
            var saved = await basketService.CreateOrUpdateBasketAsync(basket, cancellationToken);
            return Ok(saved);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result<bool>>> DeleteBasket(string id, CancellationToken cancellationToken)
        {
            var result = await basketService.DeleteBasketAsync(id, cancellationToken);
            return Ok(result);
        }
    }

}
