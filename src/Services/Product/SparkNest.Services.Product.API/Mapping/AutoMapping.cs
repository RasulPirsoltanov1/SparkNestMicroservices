using AutoMapper;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Models;

namespace SparkNest.Services.ProductAPI.Mapping
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<ProductCreateDTO, ProductDTO>().ReverseMap();
            CreateMap<ProductUpdateDTO, ProductDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Feature, FeatureDTO>().ReverseMap();
        }
    }
}
