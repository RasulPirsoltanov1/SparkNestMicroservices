using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
using SparkNest.Common.Base.Events;
using SparkNest.Common.Base.Services;
using SparkNest.Common.DTOs;
using SparkNest.Services.ProductAPI.DTOs;
using SparkNest.Services.ProductAPI.Models;
using SparkNest.Services.ProductAPI.Settings;

namespace SparkNest.Services.ProductAPI.Services
{
    public class ProductService : IProductService
    {
        IMongoCollection<Product> _productCollection;
        IMongoCollection<Category> _categoryCollection;
        IMapper _mapper;
        IDatabaseSettings _databaseSettings;
        IPublishEndpoint _publishEndpoint;
        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _publishEndpoint = publishEndpoint;
        }


        public async Task<SparkNest.Common.DTOs.Response<List<ProductDTO>>> GetAllAsync()
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
            return SparkNest.Common.DTOs.Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(products), 200);
        }

        public async Task<SparkNest.Common.DTOs.Response<ProductDTO>> GetByIdAsync(string Id)
        {
            var dbProduct = await _productCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
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
            return SparkNest.Common.DTOs.Response<ProductDTO>.Success(_mapper.Map<ProductDTO>(product),200);
        }

        public async Task<SparkNest.Common.DTOs.Response<NoContent>> UpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            var product = _mapper.Map<Product>(productUpdateDTO);
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
                UserId= product.UserId,
                ProductId = product.Id,
                UpdatedName = productUpdateDTO.Name,
            });
            return SparkNest.Common.DTOs.Response<NoContent>.Success(200);
        }

        public async Task<SparkNest.Common.DTOs.Response<NoContent>> DeleteAsync(string productId)
        {
            var result = await _productCollection.DeleteOneAsync(x => x.Id == productId);
            if (result.DeletedCount == 0)
            {
                return SparkNest.Common.DTOs.Response<NoContent>.Fail("Product not found!", 404);
            }
            return SparkNest.Common.DTOs.Response<NoContent>.Success(202);
        }
    }
}
