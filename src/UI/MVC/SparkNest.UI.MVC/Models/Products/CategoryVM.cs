namespace SparkNest.UI.MVC.Models.Product
{
    public class CategoryVM
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? SubCategoryId { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo{ get; set; }
    }
}
