namespace SparkNest.UI.MVC.Models.Baskets
{
    public class BasketVM
    {
        public string? UserId { get; set; }

        public string? DiscountCode { get; set; }

        public int? DiscountRate { get; set; }
        public decimal? TotalPrice
        {
            get
            {
                var total = _basketItems?.Sum(x => x.GetCurrentPrice * x.Quantity);
                return total;
            }
        }
        private List<BasketItemVM>? _basketItems { get; set; }
        public List<BasketItemVM>? basketItems
        {
            get
            {
                if (HasDiscount)
                {
                    _basketItems.ForEach(x =>
                    {
                        if (DiscountRate is not null && DiscountRate > 0)
                        {
                            var discountPrice = x.Price * ((decimal)DiscountRate / 100);
                            x.AppliedPrice(Math.Round(x.Price - discountPrice, 2));
                        }
                    });
                }
                return _basketItems;
            }
            set
            {
                _basketItems = value;
            }
        }

        public bool HasDiscount
        {
            get
            {
                return string.IsNullOrEmpty(DiscountCode);
            }
        }
    }
}
