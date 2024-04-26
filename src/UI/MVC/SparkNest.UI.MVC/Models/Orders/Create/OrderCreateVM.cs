namespace SparkNest.UI.MVC.Models.Orders
{
    public class OrderCreateVM
    {
        public OrderCreateVM()
        {
            OrderItems = new List<OrderItemCreateVM>();
        }
        public string BuyerId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<OrderItemCreateVM> OrderItems { get; set; }
        public AddressCreateVM Address { get; set; }
    }

}
