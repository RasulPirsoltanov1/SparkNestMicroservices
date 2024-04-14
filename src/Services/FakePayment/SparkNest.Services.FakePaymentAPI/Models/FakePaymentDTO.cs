namespace SparkNest.Services.FakePaymentAPI.Models
{
    public class FakePaymentDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderDTO Order { get; set; }
    }
}
