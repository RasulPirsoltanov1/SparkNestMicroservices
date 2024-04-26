using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.MessagesAndEvents.Base.Events
{
    public class OrderStatusChangedEvent
    {
        public string? Email { get; set; }
        public string? Status { get; set; }
        public object? Address { get; set; }
        public string? Name { get; set; }
    }
}
