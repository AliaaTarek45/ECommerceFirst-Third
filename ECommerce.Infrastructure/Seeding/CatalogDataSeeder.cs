using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerce.Infrastructure.Seeding
{
    public class CatalogDataSeeder(StoreDbContext dbContext, ILogger<CatalogDataSeeder> logger) : IDataSeeder
    {
        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
                if (pendingMigrations.Any())
                    await dbContext.Database.MigrateAsync(cancellationToken);

                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedIfEmptyAsync<ProductBrand>(seedRoot, "brands.json", cancellationToken);
                await SeedIfEmptyAsync<ProductType>(seedRoot, "types.json", cancellationToken);
                await SeedIfEmptyAsync<Product>(seedRoot, "products.json", cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Catalog data seeding failed.");
                throw;
            }
        }

        private async Task SeedIfEmptyAsync<T>(string root, string fileName, CancellationToken cancellationToken) where T : class
        {
            if (await dbContext.Set<T>().AnyAsync(cancellationToken)) return;

            var path = Path.Combine(root, fileName);
            if (!File.Exists(path))
            {
                logger.LogWarning("Seed file not found: {Path}", path);
                return;
            }

            await using var stream = File.OpenRead(path);
            var items = await JsonSerializer.DeserializeAsync<List<T>>(
                stream,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true },
                cancellationToken);

            if (items is { Count: > 0 })
                await dbContext.Set<T>().AddRangeAsync(items, cancellationToken);
        }
    }

}
