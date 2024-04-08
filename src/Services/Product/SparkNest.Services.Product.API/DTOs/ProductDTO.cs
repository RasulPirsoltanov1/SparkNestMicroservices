namespace SparkNest.Services.ProductAPI.DTOs
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string PhotoUrl { get; set; }
        public List<string>? PhotoUrls { get; set; } = new List<string>();
        public DateTime CreatedDate { get; set; }
        public FeatureDTO Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public string Description { get; set; }
        public double? Rating { get; set; }
        public double? RateCount { get; set; }
        public double? RatingCommon
        {
            get
            {
                if (RateCount > 0)
                    return Rating / RateCount;
                return 0;
            }
        }
        public int Views { get; set; }
    }
}
