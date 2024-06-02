using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShopping.API.Models
{
    public class ProductProductCategory
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int ProductCategoryID { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [ForeignKey(nameof(ProductID))]
        public Product Product { get; set; } = null!;
        [ForeignKey(nameof(ProductCategoryID))]
        public ProductCategory ProductCategory { get; set; } = null!;
    }
}
