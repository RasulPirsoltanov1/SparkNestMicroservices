using MassTransit;
using Microsoft.EntityFrameworkCore;
using SparkNest.Common.Base.Events;
using SparkNest.Services.OrderAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.EventConsumers
{
    public class ProductNameChangedEventConsumer : IConsumer<ProductNameChangedEvent>
    {
        OrderDbContext _dbContext;

        public ProductNameChangedEventConsumer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<ProductNameChangedEvent> context)
        {
            var orders = await _dbContext.Orders.Include(x => x.OrderItems).ToListAsync();
            foreach (var order in orders)
            {
                var changedOrderName = order.OrderItems.Where(x => x.ProductId == context.Message.ProductId).ToList();
                foreach (var item in changedOrderName)
                {
                    item.UpdateOrderItem(context.Message.UpdatedName,item.ProductUrl,item.Price,item.Quantity);
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
