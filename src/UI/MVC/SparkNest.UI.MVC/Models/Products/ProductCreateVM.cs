namespace SparkNest.UI.MVC.Models.Product
{
    public class ProductCreateVM
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public FeatureVM Feature { get; set; }
        public string Picture { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
    }
}
