namespace SparkNest.UI.MVC.Models.Orders
{
    public class OrderStatusVM
    {
        public int OrderId { get; set; }
        public string Error{ get; set; }
        public bool IsSuccessfull { get; set; } = true;
    }
}
