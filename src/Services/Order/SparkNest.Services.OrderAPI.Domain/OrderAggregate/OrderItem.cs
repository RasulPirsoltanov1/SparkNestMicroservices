using SparkNest.Services.OrderAPI.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Domain.OrderAggregate
{
    public class OrderItem:Entity
    {
        public OrderItem()
        {
            
        }
        public OrderItem(string productId, string productName, string productUrl, decimal price,int quantity)
        {
            ProductId = productId;
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
            Quantity = quantity;
        }

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductUrl { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        public void UpdateOrderItem(string productName, string productUrl, decimal price,int quantity)
        {
            Quantity = quantity;
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }
    }
}
