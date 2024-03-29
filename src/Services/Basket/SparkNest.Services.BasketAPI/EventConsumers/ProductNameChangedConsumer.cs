using MassTransit;
using SparkNest.Common.Base.Events;
using SparkNest.Common.Base.Services;
using SparkNest.Services.BasketAPI.Services.Abstract;

namespace SparkNest.Services.BasketAPI.EventConsumers
{
    public class ProductNameChangedConsumer : IConsumer<ProductNameChangedBasketEvent>
    {
        IBasketService _basketService;
        public ProductNameChangedConsumer(IBasketService basketService)
        {
            _basketService = basketService;
        }


        public async Task Consume(ConsumeContext<ProductNameChangedBasketEvent> context)
        {
            var basket = (await _basketService.GetBasketAsync(context.Message.UserId)).Data;
            basket.basketItems.Where(x => x.ProductId == context.Message.ProductId).ToList().ForEach(x =>
            {
                x.ProductName = context.Message.UpdatedName;
            });
            await _basketService.SaveOrUpdateAsync(basket);
        }
    }
}
