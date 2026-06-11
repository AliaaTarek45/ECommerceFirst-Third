using AutoMapper;
using ECommerce.Application.DTOs.Basket;
using ECommerce.Domain.Entities.Baskets;

namespace ECommerce.Application.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
