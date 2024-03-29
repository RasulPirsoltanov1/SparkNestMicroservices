namespace SparkNest.Services.FakePaymentAPI.Models
{
    public class OrderItemDTO
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
