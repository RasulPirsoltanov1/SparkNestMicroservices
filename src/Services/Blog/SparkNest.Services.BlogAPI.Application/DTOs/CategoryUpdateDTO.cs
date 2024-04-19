using SparkNest.Services.BlogAPI.Domain.Entities;

namespace SparkNest.Services.BlogAPI.Application.DTOs
{
    public class CategoryUpdateDTO
    {
        public virtual string Id { get; set; }
        public string? Name { get; set; }
        public List<Blog>? Blogs { get; set; }
        public int? BlogsCount { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}