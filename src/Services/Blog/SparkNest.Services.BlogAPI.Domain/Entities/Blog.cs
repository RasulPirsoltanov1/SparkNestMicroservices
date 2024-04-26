using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SparkNest.Services.BlogAPI.Domain.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Domain.Entities
{
    public class Blog : BaseEntity
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? Views { get; set; }
        public List<string>? ViewedUsers { get; set; } = new List<string>();
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId{ get; set; }
        [BsonIgnore]
        public Category? Category { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
