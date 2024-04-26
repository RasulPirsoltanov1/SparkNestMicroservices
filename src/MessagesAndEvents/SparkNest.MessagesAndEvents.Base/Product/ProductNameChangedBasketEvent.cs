﻿namespace SparkNest.MessagesAndEvents.Base.Product
{
    public class ProductNameChangedBasketEvent
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string UpdatedName { get; set; }
    }
}
