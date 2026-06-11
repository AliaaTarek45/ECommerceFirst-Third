namespace ECommerce.Domain.Contracts
{
    public interface IDataSeeder
    {
        Task SeedAsync(CancellationToken cancellationToken = default);
    }
}
