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
        public OrderItem(string productId, string productName, string productUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }

        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductUrl { get; private set; }
        public decimal Price { get; private set; }

        public void UpdateOrderItem(string productName, string productUrl, decimal price)
        {
            ProductName = productName;
            ProductUrl = productUrl;
            Price = price;
        }
    }
}
