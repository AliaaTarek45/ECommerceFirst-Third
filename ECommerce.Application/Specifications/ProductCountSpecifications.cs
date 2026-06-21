using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Specifications
{
    internal class ProductCountSpecifications : BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams)
            : base(p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId)
                && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId)
                && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.Contains(queryParams.Search!, StringComparison.CurrentCultureIgnoreCase)))
        {
        }
    }
}
