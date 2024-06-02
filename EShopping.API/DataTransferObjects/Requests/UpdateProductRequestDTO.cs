namespace EShopping.API.DataTransferObjects.Requests
{
    public class UpdateProductRequestDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; } = string.Empty;
        public float Price { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public int Status { get; set; } = 0;
        public List<int>? ProductCategories { get; set; }
    }
}
