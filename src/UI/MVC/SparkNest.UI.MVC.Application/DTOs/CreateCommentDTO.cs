using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.DTOs
{
    public class CreateCommentDTO
    {
        public string ProductId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
