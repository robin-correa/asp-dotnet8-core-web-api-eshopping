using System.ComponentModel.DataAnnotations;

namespace EShopping.API.DataTransferObjects.Requests
{
    public class StoreProductRequestDTO
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public float Price { get; set; } = 0;
        [Required]
        public int Quantity { get; set; } = 0;
        [Required]
        public int Status { get; set; } = 0;
        public List<int>? ProductCategories { get; set; }
    }
}
