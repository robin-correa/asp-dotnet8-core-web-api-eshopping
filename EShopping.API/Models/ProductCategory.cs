using System.ComponentModel.DataAnnotations;

namespace EShopping.API.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Status { get; set; } = 0;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        // Navigation property for many-to-many relationship with Product
        public List<Product> Products { get; set; } = [];
    }
}
