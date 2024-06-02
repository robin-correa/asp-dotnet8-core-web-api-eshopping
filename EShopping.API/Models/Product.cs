using System.ComponentModel.DataAnnotations;

namespace EShopping.API.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        public int Status { get; set; } = 0;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        // Navigation property for many-to-many relationship with ProductCategory
        public List<ProductCategory>? ProductCategories { get; set; } = [];
    }
}
