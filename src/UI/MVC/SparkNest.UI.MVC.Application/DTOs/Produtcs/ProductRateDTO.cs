using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Application.DTOs.Produtcs
{
    public class ProductRateDTO
    {
        public string? ProductId { get; set; }
        public string? UserId { get; set; }
        public int? Rating { get; set; }
    }
}
