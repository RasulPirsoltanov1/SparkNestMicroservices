using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SparkNest.Services.BlogAPI.Domain.Entities;

namespace SparkNest.Services.BlogAPI.Application.DTOs
{
    public class BlogDTO
    {
        public virtual string Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? Views { get; set; }
        public string? CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? PhotoUrl { get; set; }
        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
    }
}