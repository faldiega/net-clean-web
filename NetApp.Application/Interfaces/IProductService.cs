using NetApp.Application.DTOs;
using NetApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId);
        Task CreateAsync(CreateProductDto dto);
        Task UpdateAsync(UpdateProductDto dto);
        Task DeleteAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
