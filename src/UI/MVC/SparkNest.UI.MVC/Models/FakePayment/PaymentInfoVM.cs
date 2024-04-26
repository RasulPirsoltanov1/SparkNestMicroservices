using SparkNest.UI.MVC.Models.Orders;

namespace SparkNest.UI.MVC.Models.FakePayment
{
    public class PaymentInfoVM
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public decimal? TotalPrice{ get; set; }
        public OrderCreateVM? Order{ get; set; }
    }
}
