using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.MailServiceAPI.Models
{
    public class Subscriber : BaseEntity
    {
        public string? Email { get; set; }
    }
}
