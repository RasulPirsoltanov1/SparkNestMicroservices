using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.DTOs.Blog
{
    public class BlogCategoryDTO
    {
        public virtual string Id { get; set; }
        public string? Name { get; set; }
        public List<BlogDTO>? Blogs { get; set; }
        public int? BlogsCount { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}
