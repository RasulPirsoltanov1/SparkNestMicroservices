namespace SparkNest.Services.BasketAPI.DTOs
{
    public class BasketDto
    {
        public string? UserId { get; set; }

        public string? DiscountCode { get; set; }

        public int? DiscountRate { get; set; }
        public List<BasketItemDTO>? basketItems { get; set; }

        public decimal? TotalPrice
        {
            get => basketItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
