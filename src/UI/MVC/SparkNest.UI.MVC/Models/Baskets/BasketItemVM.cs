namespace SparkNest.UI.MVC.Models.Baskets
{
    public class BasketItemVM
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountAppliedPrice { get; set; }
        public decimal GetCurrentPrice
        {
            get
            {
                if (DiscountAppliedPrice != null)
                {
                    return DiscountAppliedPrice.Value;
                }
                return Price;
            }
        }
        public void AppliedPrice(decimal discountPrice)
        {
            DiscountAppliedPrice = discountPrice;
        }

    }
}
