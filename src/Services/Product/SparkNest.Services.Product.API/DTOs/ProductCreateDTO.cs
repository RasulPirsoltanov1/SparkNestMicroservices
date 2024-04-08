using MongoDB.Bson.Serialization.Attributes;
using SparkNest.Services.ProductAPI.Models;

namespace SparkNest.Services.ProductAPI.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public FeatureDTO Feature { get; set; }
        public string PhotoUrl { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string Description { get; set; }
        public List<string>? PhotoUrls { get; set; } = new List<string>();
        public double? Rating { get; set; }
        public double? RateCount { get; set; }
    }
}
