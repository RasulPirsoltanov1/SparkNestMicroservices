namespace SparkNest.Services.BasketAPI.DTOs
{
    public class BasketItemDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }

    public class BasketDTO
    {
        public BasketDTO()
        {
            BasketItems = new List<BasketItemDTO>();
        }
        public string UserId { get; set; }
        public int DiscountCode { get; set; }
        public decimal? Total
        {
            get
            {
                var total = BasketItems?.Sum(x => x.Price*x.Quantity);
                return total;
            }
        }
        public List<BasketItemDTO>? BasketItems { get; set; }
    }
}
