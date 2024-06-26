﻿namespace SparkNest.Services.ProductAPI.DTOs
{
    public class ProductUpdateDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string UserId { get; set; }
        public FeatureDTO Feature { get; set; }
        public string CategoryId { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public List<string>? PhotoUrls { get; set; } = new List<string>();
        //public double? Rating { get; set; }
        //public double? RateCount { get; set; }
        public double? DiscountPercentage { get; set; }
        public double? PriceDiscount { get; set; }
        public int? Views { get; set; }
        public double? Rating { get; set; }
        public double? RateCount { get; set; }
        public List<string>? RatedUsers { get; set; } = new List<string>();
    }
}
