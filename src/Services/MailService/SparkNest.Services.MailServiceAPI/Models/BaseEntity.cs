using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.MailServiceAPI.Models
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public virtual DateTime? CreateDate { get; set; }
        //[BsonRepresentation(BsonType.DateTime)]
        public virtual DateTime? UpdateDate { get; set; }
    }
}
