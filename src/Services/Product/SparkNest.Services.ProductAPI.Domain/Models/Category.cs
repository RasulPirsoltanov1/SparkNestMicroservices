using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security.Principal;

namespace SparkNest.Services.ProductAPI.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string? SubCategoryId { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
