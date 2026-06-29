using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Application.DTOs
{
    // Untuk menampilkan data Category ke View
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int ProductCount { get; set; }
    }

    // Untuk form Create
    public class CreateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
    }

    // Untuk form Edit
    public class UpdateCategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
