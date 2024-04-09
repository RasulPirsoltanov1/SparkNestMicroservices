using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.CommentAPI.Domain.Entites
{
    public class Comment : BaseEntity
    {
        public string? ProductId { get; set; }
        public string? Content { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
