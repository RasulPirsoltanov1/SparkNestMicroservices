using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.DTOs.Blog
{
    public class BlogDTO
    {
        public virtual string Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? Views { get; set; }
        public string? CategoryId { get; set; }
        public BlogCategoryDTO? Category { get; set; }
        public string? PhotoUrl { get; set; }
        public string? PhotoFileStockUrl { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}
