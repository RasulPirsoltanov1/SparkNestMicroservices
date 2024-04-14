namespace SparkNest.UI.MVC.Models.Orders
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public int Status { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public AddressVM? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }
    }
}
