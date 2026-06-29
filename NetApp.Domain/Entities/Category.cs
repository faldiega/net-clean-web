using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
