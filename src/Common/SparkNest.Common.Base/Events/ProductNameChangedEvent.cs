using System;
using System.Collections.Generic;
using System.Text;

namespace SparkNest.Common.Base.Events
{
    public class ProductNameChangedEvent
    {
        public string ProductId { get; set; }
        public string UpdatedName { get; set; }
    }
}
