using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NetApp.Application.DTOs;
using NetApp.Application.Interfaces;
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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        protected readonly AppDbContext _ctx;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _ctx = context;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbSet.AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }

}
