namespace SparkNest.Services.ProductAPI.DTOs
{
    public class ProductUpdateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string UserId { get; set; }
        public FeatureDTO Feature { get; set; }
        public string CategoryId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
    }
}
