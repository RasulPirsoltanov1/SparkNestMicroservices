using SparkNest.Services.OrderAPI.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public Order()
        {

        }
        public Order(string buyerId, Address address)
        {
            BuyerId = buyerId;
            Address = address;
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
        }
        public DateTime? CreatedDate { get; set; }
        public Address Address { get; set; }
        public string BuyerId { get; set; }
        public int Status { get; set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public void AddOrderItem(string productId, string productName, string productUrl, decimal price, int quantity)
        {
            var exisItem = _orderItems.Any(x => x.ProductId == productId);
            if (!exisItem)
            {
                _orderItems.Add(new OrderItem(productId, productName, productUrl, price, quantity));
            }
        }

        public decimal TotalPrice() => _orderItems.Sum(x => x.Price);
    }
}
