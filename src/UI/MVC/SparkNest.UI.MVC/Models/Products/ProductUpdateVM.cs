using System.ComponentModel.DataAnnotations;

namespace SparkNest.UI.MVC.Models.Product
{
    public class ProductUpdateVM
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
         
        public string? UserId { get; set; }

        public string? PhotoUrl { get; set; }
        public IFormFile Photo{ get; set; }
        public FeatureVM Feature { get; set; }
        [Required]
        public string CategoryId { get; set; }

    }
}
