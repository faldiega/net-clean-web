using Microsoft.EntityFrameworkCore;
using NetApp.Domain.Entities;
using NetApp.Domain.Interfaces;
using NetApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        protected readonly AppDbContext ctx;
        public ProductRepository(AppDbContext context) : base(context)
        {
            ctx = context;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await (from p in ctx.Product
                          join c in ctx.Category
                          on p.CategoryId equals c.CategoryId
                          select new Product
                          {
                              ProductId = p.ProductId,
                              Name = p.Name,
                              Price = p.Price,
                              Stock = p.Stock,
                              CreatedDate = p.CreatedDate,
                              CategoryId = p.CategoryId, 
                              Category = c
                          }).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(p => p.Name.ToLower() == name.ToLower());
        }
    }

}
