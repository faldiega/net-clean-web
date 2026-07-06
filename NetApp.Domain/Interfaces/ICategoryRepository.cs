using NetApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // Operasi khusus Category di luar CRUD standar
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllCategoryAsync();
    }
}
