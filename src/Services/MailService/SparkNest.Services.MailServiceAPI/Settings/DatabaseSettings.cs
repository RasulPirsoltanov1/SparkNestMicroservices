using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.MailServiceAPI.Settings
{
    public class DatabaseSettings
    {
        public string? SubscriberCollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
