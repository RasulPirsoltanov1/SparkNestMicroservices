namespace SparkNest.UI.MVC.Models.Product
{
    public class ProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoFileStockUrl { get; set; }
        public List<string>? PhotoUrls { get; set; } = new List<string>();
        public List<string>? PhotoFileStockUrls { get; set; } = new List<string>();
        public DateTime CreatedDate { get; set; }
        public FeatureVM? Feature { get; set; }
        public string? CategoryId { get; set; }
        public CategoryVM? Category { get; set; }
        public string? Description { get; set; }
        public string? ShortDescription
        {
            get
            {
                return Description.Length > 100 ? Description.Substring(0, 100) : Description;
            }
        }
        public double? DiscountPercentage { get; set; }
        public double? PriceDiscount { get; set; }
        public double? Rating { get; set; }
        public double? RateCount { get; set; }
        public double? RatingCommon { get; set; }
        public int Views { get; set; } = 0;


    }
}
