using NetApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        // Operasi khusus Product di luar CRUD standar
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<bool> ExistsByNameAsync(string name);
    }
}
