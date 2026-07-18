using NetApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Application.Interfaces
{
    public interface ICategoryQueryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoryAsync();
    }
}
