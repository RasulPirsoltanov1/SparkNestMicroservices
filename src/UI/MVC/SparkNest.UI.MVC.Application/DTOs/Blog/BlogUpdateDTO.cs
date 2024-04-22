using Microsoft.AspNetCore.Http;

namespace SparkNest.UI.MVC.Application.DTOs.Blog
{
    public class BlogUpdateDTO
    {
        public virtual string Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CategoryId { get; set; }
        public string? PhotoUrl { get; set; }
        public IFormFile? Photo { get; set; }
    }

}
