using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Core.Authentication;
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
        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            _mapper = mapper;
            _databaseSettings = databaseSettings;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }


        public async Task<Response<List<ProductDTO>>> GetAllAsync()
        {
            var products = await _productCollection.Find(x => true).ToListAsync();
            if (products == null)
            {
                return Response<List<ProductDTO>>.Fail("Have not any product!", 404);
            }
            foreach (var product in products)
            {
                product.Category = _categoryCollection.Find(x => x.Id == product.CategoryId).FirstOrDefault();
            }
            return Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(products), 200);
        }

        public async Task<Response<ProductDTO>> GetByIdAsync(string Id)
        {
            var dbProduct = await _productCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            if (dbProduct == null)
            {
                return Response<ProductDTO>.Fail("Have not any product!", 404);
            }
            dbProduct.Category = _categoryCollection.Find(x => x.Id == dbProduct.CategoryId).FirstOrDefault();
            return Response<ProductDTO>.Success(_mapper.Map<ProductDTO>(dbProduct), 200);
        }

        public async Task<Response<List<ProductDTO>>> GetAllByUserIdAsync(string UserId)
        {
            var products = await _productCollection.Find(x => x.UserId == UserId).ToListAsync();
            if (products == null)
            {
                return Response<List<ProductDTO>>.Fail("Have not any product!", 404);
            }
            foreach (var product in products)
            {
                product.Category = _categoryCollection.Find(x => x.Id == product.CategoryId).FirstOrDefault();
            }
            return Response<List<ProductDTO>>.Success(_mapper.Map<List<ProductDTO>>(products), 200);
        }

        public async Task<Response<ProductDTO>> CreateAsync(ProductCreateDTO productCreateDTO)
        {
            var product = _mapper.Map<Product>(productCreateDTO);
            product.CreatedDate = DateTime.Now;
            await _productCollection.InsertOneAsync(product);
            return Response<ProductDTO>.Success(_mapper.Map<ProductDTO>(product),200);
        }

        public async Task<Response<NoContent>> UpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            var product = _mapper.Map<Product>(productUpdateDTO);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.Id == productUpdateDTO.Id, product);
            if (result == null)
            {
                return Response<NoContent>.Fail("Product not found!", 404);
            }
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteAsync(ProductUpdateDTO productUpdateDTO)
        {
            var product = _mapper.Map<Product>(productUpdateDTO);
            var result = await _productCollection.DeleteOneAsync(x => x.Id == productUpdateDTO.Id);
            if (result.DeletedCount == 0)
            {
                return Response<NoContent>.Fail("Product not found!", 404);
            }
            return Response<NoContent>.Success(202);
        }
    }
}
