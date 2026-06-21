using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Product;
using ECommerce.Application.Specifications;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Services
{
    internal class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken cancellationToken = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(cancellationToken);
            return Result<IReadOnlyList<BrandDto>>.Ok(_mapper.Map<IReadOnlyList<BrandDto>>(brands));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken cancellationToken = default)
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(cancellationToken);
            return Result<IReadOnlyList<TypeDto>>.Ok(_mapper.Map<IReadOnlyList<TypeDto>>(types));
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken cancellationToken = default)
        {

            var repo = _unitOfWork.GetRepository<Product, int>();

            var products = await repo.GetAllAsync(new ProductWithBrandAndTypeSpecifications(queryParams), cancellationToken);
            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return Result<PaginatedResult<ProductDto>>.Ok(new PaginatedResult<ProductDto>(queryParams.PageNumber, queryParams.PageSize, products.Count(), data));
        }
        public async Task<Result<ProductDto>> GetProductAsync(int id, CancellationToken cancellationToken = default)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id), cancellationToken);

            if (product is null)
                return Result<ProductDto>.Fail(Error.NotFound("Product.NotFound", $"Product with id {id} was not found."));

            return _mapper.Map<ProductDto>(product);
        }
    }
}