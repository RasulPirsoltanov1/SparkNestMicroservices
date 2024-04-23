using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.MessagesAndEvents.Base.SubscriberNotification
{
    public class SubscriberNotificationEvent
    {
        public string? Title { get; set; }
        public string? CategoryName { get; set; }
        public string? Content { get; set; }
    }
}
