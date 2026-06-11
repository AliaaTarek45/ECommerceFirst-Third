using AutoMapper;
using ECommerce.Application.DTOs.Product;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dst => dst.ProductBrand, opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dst => dst.ProductType, opt => opt.MapFrom(src => src.ProductType.Name))
                .ForMember(dst => dst.PictureUrl, opt => opt.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }

}
