using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCom.Core.Models
{
    public class Product
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="Enter Product Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Product Description")]
        public string Description { get; set; }

        [Range(0,1000)]
        public Decimal Price { get; set; }

        public string Category { get; set; }

        public string Image { get; set; }

        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
