using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Product;

namespace ECommerce.Application.Abstractions
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken cancellationToken = default);
        Task<Result<ProductDto>> GetProductAsync(int id, CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken cancellationToken = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken cancellationToken = default);


    }
}