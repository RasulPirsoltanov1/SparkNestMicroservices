using MassTransit;
using Microsoft.EntityFrameworkCore.Storage.Json;
using SparkNest.Common.Base.Messages;
using SparkNest.Services.OrderAPI.Domain.OrderAggregate;
using SparkNest.Services.OrderAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.OrderAPI.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {
        OrderDbContext _dbContext;

        public CreateOrderMessageCommandConsumer(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var address = new Address(context.Message.Province, context.Message.Street, context.Message.District, context.Message.Line, context.Message.ZipCode);

            Order order = new Order(context.Message.BuyerId, address);

            context.Message.OrderItems.ForEach(x =>
            {
                order.AddOrderItem(x.ProductId, x.ProductName, x.ProductUrl, x.Price,x.Quantity);
            });
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
