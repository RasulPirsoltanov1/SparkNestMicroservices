using MongoDB.Bson.Serialization.Attributes;

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
    }
}
