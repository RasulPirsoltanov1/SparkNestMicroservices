﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SparkNest.Services.ProductAPI.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string PhotoUrl { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }
        public Feature Feature { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
        public string Description { get; set; }
        public List<string>? PhotoUrls { get; set; } = new List<string>();
        public double? Rating { get; set; }
        public double? RateCount { get; set; }
        public List<string> RatedUsers { get; set; } = new List<string>();
        public int Views { get; set; }
        public double? DiscountPercentage { get; set; }
        public double? PriceDiscount { get; set; }
    }
}
