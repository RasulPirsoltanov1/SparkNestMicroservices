﻿namespace SparkNest.UI.MVC.Models.Orders
{
    public class OrderItemCreateVM
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public decimal Price { get; set; }
    }

}
