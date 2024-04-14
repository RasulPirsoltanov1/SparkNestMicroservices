using System;
using System.Collections.Generic;
using System.Text;

namespace SparkNest.Common.Base.Messages
{
    public class CreateOrderMessageCommand
    {
        public CreateOrderMessageCommand()
        {
            OrderItems = new List<OrderItem>();
        }
        public string BuyerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string Province { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Line { get; set; }
        public string ZipCode { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }


    }
    public class OrderItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
 
