﻿namespace SparkNest.UI.MVC.Models.Orders
{
    public class CheckoutInfoVM
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Line { get; set; }
        public string ZipCode { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
