namespace ShoppingApp.API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public string Thumbnail { get; set; }
        public decimal Price { get; set; }
    }
}
