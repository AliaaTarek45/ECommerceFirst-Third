using ECommerce.Application.Abstractions;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ApplicationServicesRegistration).Assembly);

            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
