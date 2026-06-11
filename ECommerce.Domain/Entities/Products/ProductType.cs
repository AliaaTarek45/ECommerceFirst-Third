using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities.Products
{
    public class ProductType : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
    }
}
