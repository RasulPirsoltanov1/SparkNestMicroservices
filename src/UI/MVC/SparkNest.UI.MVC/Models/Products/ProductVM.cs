namespace SparkNest.UI.MVC.Models.Product
{
    public class ProductVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public FeatureVM Feature { get; set; }
        public string CategoryId { get; set; }
        public CategoryVM Category { get; set; }
        public string Description { get; set; }
    }
}
