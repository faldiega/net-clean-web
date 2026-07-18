using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public virtual Category Category { get; set; } = null!;

    }
}
