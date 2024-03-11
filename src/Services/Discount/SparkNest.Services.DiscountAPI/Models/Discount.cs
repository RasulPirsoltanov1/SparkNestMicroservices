using Dapper.Contrib.Extensions;

namespace SparkNest.Services.DiscountAPI.Models
{
    [Table("dicount")]
    public class Discount
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int Rate { get; set; }
        public string Code { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
