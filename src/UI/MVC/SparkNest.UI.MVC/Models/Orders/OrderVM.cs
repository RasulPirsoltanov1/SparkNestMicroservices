namespace SparkNest.UI.MVC.Models.Orders
{
    public class OrderVM
    {
        public string BuyerId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }
    }
}
