using System.ComponentModel.DataAnnotations;

namespace SparkNest.UI.MVC.Models.Product
{
    public class ProductCreateVM
    {
        //[Required]
        public string Name { get; set; }
        //[Required]
        public decimal Price { get; set; }
        public string? UserId { get; set; }
        //[Required]
        public FeatureVM Feature { get; set; }
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoUrl{ get; set; }
    }
}
