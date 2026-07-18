using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetApp.Application.Interfaces;
using NetApp.Domain.Interfaces;
using NetApp.Infrastructure.Data;
using NetApp.Infrastructure.Persistence;
using NetApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Connection - EF Core
            services.AddDbContext<AppDbContext>((sp, opt) =>
            {
                var factory = sp.GetRequiredService<IDbConnectionFactory>();
                opt.UseSqlServer(factory.GetConnectionStringDefault("DefaultDB"));
            });

            // Register Connection - Dapper
            services.AddScoped<DapperContext>();
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

            // Register Repositories - EF Core
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            // Register Repositories - Dapper
            services.AddScoped<ICategoryQueryService, CategoryQueryRepository>();


            return services;
        }
    }
}
