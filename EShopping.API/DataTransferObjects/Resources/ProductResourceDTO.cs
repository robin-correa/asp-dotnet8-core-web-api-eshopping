namespace EShopping.API.DataTransferObjects.Resources
{
    public class ProductResourceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public List<ProductCategoryResourceDTO>? ProductCategories { get; set; }
    }
}
