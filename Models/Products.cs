namespace ProductAPI.Models
{
    public class Product
    {
        public int Id { get; set; } // Thuộc tính Id
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}