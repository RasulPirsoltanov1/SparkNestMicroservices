﻿using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using SparkNest.Common.Base.Events;
using SparkNest.Common.Base.Services;
using SparkNest.Common.DTOs;
using SparkNest.Services.ProductAPI.Application.Abstrctions;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Models;
using SparkNest.Services.ProductAPI.Settings;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SparkNest.Services.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        IMongoCollection<Product> _productCollection;
        IMongoCollection<Category> _categoryCollection;
        IMapper _mapper;
        IDatabaseSettings _databaseSettings;
        IPublishEndpoint _publishEndpoint;
        IRedisService<List<Product>> _redisService;
        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings, IPublishEndpoint publishEndpoint, IRedisService<List<Product>> redisService)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _publishEndpoint = publishEndpoint;
            _redisService = redisService;
        }
        public async Task<SparkNest.Common.DTOs.Response<List<ProductDTO>>> GetAllForUpdateRedisAsync()
        {
            var products = await _productCollection.Find(x => true).ToListAsync();
            if (products == null)
            {
                return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Fail("Have not any product!", 404);
            }
            foreach (var product in products)
            {
                product.Category = _categoryCollection.Find(x => x.Id == product.CategoryId).FirstOrDefault();
            }
            await _redisService.SaveStringAsync(nameof(Product), JsonSerializer.Serialize(products));
            return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(products), 200);
        }

        public async Task<SparkNest.Common.DTOs.Response<List<ProductDTO>>> GetAllAsync()
        {
            var cacheProducts = await _redisService.GetStringAsync(nameof(Product));
            if (cacheProducts != null)
            {
                return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(cacheProducts), 200);
            }
            var products = await _productCollection.Find(x => true).ToListAsync();
            if (products == null)
            {
                return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Fail("Have not any product!", 404);
            }
            foreach (var product in products)
            {
                product.Category = _categoryCollection.Find(x => x.Id == product.CategoryId).FirstOrDefault();
            }
            await _redisService.SaveStringAsync(nameof(Product), JsonSerializer.Serialize(products));
            return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(products), 200);
        }

        public async Task<SparkNest.Common.DTOs.Response<ProductDTO>> GetByIdAsync(string Id)
        {
            var dbProduct = await _productCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            dbProduct.Views += 1;
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == dbProduct.Id, dbProduct);
            if (dbProduct == null)
            {
                return SparkNest.Common.DTOs.Response<ProductDTO>.Fail("Have not any product!", 404);
            }
            dbProduct.Category = _categoryCollection.Find(x => x.Id == dbProduct.CategoryId).FirstOrDefault();
            return SparkNest.Common.DTOs.Response<ProductDTO>.Success(_mapper.Map<ProductDTO>(dbProduct), 200);
        }

        public async Task<SparkNest.Common.DTOs.Response<List<ProductDTO>>> GetAllByUserIdAsync(string UserId)
        {
            var products = await _productCollection.Find(x => x.UserId == UserId).ToListAsync();
            if (products == null)
            {
                return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Fail("Have not any product!", 404);
            }
            foreach (var product in products)
            {
                product.Category = _categoryCollection.Find(x => x.Id == product.CategoryId).FirstOrDefault();
            }
            return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(products), 200);
        }

        public async Task<SparkNest.Common.DTOs.Response<ProductDTO>> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            product.CreatedDate = DateTime.Now;
            await _productCollection.InsertOneAsync(product);
            await GetAllForUpdateRedisAsync();
            return SparkNest.Common.DTOs.Response<ProductDTO>.Success(_mapper.Map<ProductDTO>(product), 200);
        }

        public async Task<SparkNest.Common.DTOs.Response<NoContent>> UpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            var product = _mapper.Map<Product>(productUpdateDTO);
            productUpdateDTO.Views = product.Views;
            productUpdateDTO.Rating = product.Rating;
            productUpdateDTO.RateCount = product.RateCount;
            productUpdateDTO.RatedUsers = product.RatedUsers;
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == productUpdateDTO.Id, product);
            if (result == null)
            {
                return SparkNest.Common.DTOs.Response<NoContent>.Fail("Product not found!", 404);
            }
            await _publishEndpoint.Publish<ProductNameChangedEvent>(new ProductNameChangedEvent
            {
                ProductId = product.Id,
                UpdatedName = productUpdateDTO.Name,
            });
            await _publishEndpoint.Publish<ProductNameChangedBasketEvent>(new ProductNameChangedBasketEvent
            {
                UserId = product.UserId,
                ProductId = product.Id,
                UpdatedName = productUpdateDTO.Name,
            });
            await GetAllForUpdateRedisAsync();

            return SparkNest.Common.DTOs.Response<NoContent>.Success(200);
        }

        public async Task<SparkNest.Common.DTOs.Response<NoContent>> DeleteAsync(string productId)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == productId);
            if (result.DeletedCount == 0)
            {
                return SparkNest.Common.DTOs.Response<NoContent>.Fail("Product not found!", 404);
            }
            await GetAllForUpdateRedisAsync();

            return SparkNest.Common.DTOs.Response<NoContent>.Success(202);
        }

        public async Task<SparkNest.Common.DTOs.Response<NoContent>> DeletePhotoAsync(string productId, string photoUrl)
        {
            var dbProduct = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();

            // Check if dbProduct or dbProduct.PhotoUrls is null
            if (dbProduct != null && dbProduct.PhotoUrls != null)
            {
                var url = dbProduct.PhotoUrls.FirstOrDefault(x => ExtractGUID(x) == ExtractGUID(photoUrl));
                await Console.Out.WriteLineAsync(url);
                dbProduct.PhotoUrls.Remove(url); // Remove the specified photoUrl from the list
                var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == dbProduct.Id, dbProduct);
                await GetAllForUpdateRedisAsync();

                return SparkNest.Common.DTOs.Response<NoContent>.Success(202);
            }
            else
            {
                // Handle the case when dbProduct or dbProduct.PhotoUrls is null
                return SparkNest.Common.DTOs.Response<NoContent>.Fail("Product or PhotoUrls not found", 404);
            }
        }

        public async Task<SparkNest.Common.DTOs.Response<NoContent>> AddPhotosToGallery(string productId, List<string> photoUrl)
        {
            var dbProduct = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
            if (dbProduct != null)
            {
                dbProduct.PhotoUrls.AddRange(photoUrl);
                var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == dbProduct.Id, dbProduct);
                return SparkNest.Common.DTOs.Response<NoContent>.Success(202);
            }
            else
            {
                // Handle the case when dbProduct or dbProduct.PhotoUrls is null
                return SparkNest.Common.DTOs.Response<NoContent>.Fail("Product or PhotoUrls not found", 404);
            }
        }

        static string ExtractGUID(string url)
        {
            // Define the pattern for finding GUID-like strings
            string pattern = @"([a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12})";

            // Search for the pattern in the URLs
            Match match = Regex.Match(url, pattern);

            // If a match is found, return the GUID part
            if (match.Success)
            {
                return match.Value;
            }

            // Return empty string if no match is found
            return "";
        }

        public async Task<Common.DTOs.Response<NoContent>> RateProductAsync(ProductRateDTO productRateDTO)
        {
            var dbProduct = await _productCollection.Find(x => x.Id == productRateDTO.ProductId).FirstOrDefaultAsync();
            if (dbProduct != null)
            {
                if (dbProduct.RatedUsers.FirstOrDefault(x => x == productRateDTO.UserId) is not null)
                {
                    return SparkNest.Common.DTOs.Response<NoContent>.Success(200);
                }
                if (dbProduct.Rating == null)
                {
                    dbProduct.Rating = productRateDTO.Rating;
                }
                else
                {
                    dbProduct.Rating += productRateDTO.Rating;
                }
                if (dbProduct.RateCount == null)
                {
                    dbProduct.RateCount = 1;
                }
                else
                {
                    dbProduct.RateCount += 1;
                }
                dbProduct.RatedUsers.Add(productRateDTO.UserId);
                var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == dbProduct.Id, dbProduct);
                await GetAllForUpdateRedisAsync();
                return SparkNest.Common.DTOs.Response<NoContent>.Success(202);
            }
            else
            {
                // Handle the case when dbProduct or dbProduct.PhotoUrls is null
                return SparkNest.Common.DTOs.Response<NoContent>.Fail("Not found", 404);
            }
        }
    }
}
