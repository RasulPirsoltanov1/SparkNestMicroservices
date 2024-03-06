﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using SparkNest.Services.ProductAPI.Models;

namespace SparkNest.Services.ProductAPI.DTOs
{
    public class CategoryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
