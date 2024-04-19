using SparkNest.Services.BlogAPI.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Domain.Entities
{
    public class Category:BaseEntity
    {
        public string? Name{ get; set; }
        public List<Blog>? Blogs { get; set; }
    }
}
