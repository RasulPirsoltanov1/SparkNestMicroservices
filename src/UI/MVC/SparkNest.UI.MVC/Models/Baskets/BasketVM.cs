using SparkNest.UI.MVC.Models.Baskets;

public class BasketVM
{
    public BasketVM()
    {
        BasketItems = new List<BasketItemVM>(); // _basketItems yerine BasketItems kullanıldı
    }

    public string? UserId { get; set; }
    public string DiscountCode { get; set; }
    public int? DiscountRate { get; set; }

    public decimal? TotalPrice
    {
        get
        {
            var total = BasketItems?.Sum(x => x.GetCurrentPrice * x.Quantity);
            return total;
        }
    }
    private List<BasketItemVM> _basketItems;

    public List<BasketItemVM> BasketItems
    {
        get
        {
            if (HasDiscount)
            {
                //Örnek kurs fiyat 100 TL indirim %10
                _basketItems.ForEach(x =>
                {
                    var discountPrice = x.Price * ((decimal)DiscountRate.Value / 100);
                    x.AppliedPrice(Math.Round(x.Price - discountPrice, 2));
                });
            }
            return _basketItems;
        }
        set
        {
            _basketItems = value;
        }
    }
    //public List<BasketItemVM>? BasketItems { get; set; } // _basketItems kullanımı kaldırıldı

    public bool HasDiscount
    {
        get => !string.IsNullOrEmpty(DiscountCode) && DiscountRate.HasValue;
    }
    public void CancelDiscount()
    {
        DiscountRate = null;
        DiscountCode = null;
    }
}
