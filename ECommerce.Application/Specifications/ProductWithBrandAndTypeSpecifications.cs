using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams)
            : base(p =>
                (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                && (string.IsNullOrWhiteSpace(queryParams.Search)
                    || p.Name.ToLower().Contains(queryParams.Search!.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc: AddOrderBy(p => p.Name); break;
                case ProductSortingOptions.NameDesc: AddOrderByDescending(p => p.Name); break;
                case ProductSortingOptions.PriceAsc: AddOrderBy(p => p.Price); break;
                case ProductSortingOptions.PriceDesc: AddOrderByDescending(p => p.Price); break;
                default: AddOrderBy(p => p.Name); break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageNumber);
        }

        public ProductWithBrandAndTypeSpecifications(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }

}
