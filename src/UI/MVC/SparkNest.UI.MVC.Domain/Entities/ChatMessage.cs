using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.UI.MVC.Domain.Entities
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? ClientConnectionId { get; set; }

        public string? Message { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
        public bool? IsAnswered { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
