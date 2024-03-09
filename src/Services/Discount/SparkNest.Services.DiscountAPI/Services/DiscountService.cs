using Dapper;
using Npgsql;
using SparkNest.Common.DTOs;
using SparkNest.Services.DiscountAPI.Models;
using System.Data;

namespace SparkNest.Services.DiscountAPI.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> DeleteAsync(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new
            {
                Id=id
            });
            if (status > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("discount not found!", 404);
        }

        public async Task<Response<List<Discount>>> GetAllAsync()
        {
            var discounts = await _dbConnection.QueryAsync<Discount>("Select * from discount");
            return Response<List<Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Discount>> GetByCodeAndUserIdAsync(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Discount>("SELECT * FROM discount where userid=@userId and code=@code",new
            {
                code = code,
                userId = userId
            }); 
            if(discount == null)
            {
                return Response<Discount>.Fail("discount not found",404);
            }
            return Response<Discount>.Success(discount.SingleOrDefault(), 200);
        }

        public async Task<Response<Discount>> GetByIdAsync(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Discount>("select * from discount where Id=@Id", new
            {
                Id = id
            })).SingleOrDefault();
            if (discount == null)
            {
                return Response<Discount>.Fail("discount does not exists!", 404);
            }
            return Response<Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> SaveAsync(Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
            if (status > 0)
            {
                return Response<NoContent>.Success(200);
            }
            return Response<NoContent>.Fail(string.Join(",", discount), 500);
        }

        public async Task<Response<NoContent>> UpdateAsync(Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update discount set userid=@UserId,rate=@Rate,code=@Code");
            if (status > 0)
            {
                return Response<NoContent>.Success(200);
            }
            return Response<NoContent>.Fail("errors occured while discount adding to database",500);
        }
    }
}
