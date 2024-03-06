namespace SparkNest.Services.ProductAPI.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedDate { get; set; }
        public FeatureDTO Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public string Description { get; set; }
    }
}
