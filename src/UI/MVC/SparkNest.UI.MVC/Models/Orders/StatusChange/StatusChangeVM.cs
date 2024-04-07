using SparkNest.Common.DTOs;

namespace SparkNest.UI.MVC.Models.Orders.StatusChange
{
    public class StatusChangeVM
    {
        public int OrderId { get; set; }
        public int Status { get; set; }
    }
}
