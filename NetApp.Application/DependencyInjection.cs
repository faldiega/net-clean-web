using Microsoft.Extensions.DependencyInjection;
using NetApp.Application.Interfaces;
using NetApp.Application.Services;

namespace NetApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
