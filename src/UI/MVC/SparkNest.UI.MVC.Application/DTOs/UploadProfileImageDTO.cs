using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.DTOs
{

    public class UploadProfileImageDTO
    {
        [Required]
        public string? UserNameOrEmail { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile? Image{ get; set; }
    }
}
