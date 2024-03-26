namespace SparkNest.Services.BasketAPI.DTOs
{
    public class BasketItemDTO
    {
        public string? ProductId { get; set; }
        public int? Quantity { get; set; }

        public string? ProductName { get; set; }

        public decimal? Price { get; set; }
    }
}
