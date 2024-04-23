using MongoDB.Bson;
using MongoDB.Driver;
using SparkNest.Common.DTOs;
using SparkNest.Services.BlogAPI.Application.Interfaces;
using SparkNest.Services.BlogAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkNest.Services.BlogAPI.Infrastructure.Concretes
{
    public class SubscriberService : ISubscriberService
    {
        IMongoDatabase _database;
        IMongoCollection<Subscriber> _subcribers;

        public SubscriberService(IMongoDatabase database)
        {
            _database = database;
            _subcribers = _database.GetCollection<Subscriber>(nameof(Subscriber));
        }

        public async Task<Response<bool>> CreateAsync(Subscriber subscriber)
        {
            try
            {
                var subscriberDb= await _subcribers.FindAsync(x=>x.Email== subscriber.Email);
                if (subscriberDb is not null)
                    return Response<bool>.Fail("User already exists!", 400); ;
                subscriber.CreateDate = DateTime.Now;
                await _subcribers.InsertOneAsync(subscriber);
                return Response<bool>.Success(true, 200);
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<List<Subscriber>>> GetAllAsync()
        {
            try
            {
                var subscribers = await _subcribers.Find(new BsonDocument()).ToListAsync();
                return Response<List<Subscriber>>.Success(subscribers ?? new List<Subscriber>(), 200);
            }
            catch (Exception ex)
            {
                return Response<List<Subscriber>>.Fail(ex.Message, 500);
            }
        }

        public async Task<Response<Subscriber>> GetByIdAsync(string subscriberId)
        {
            try
            {
                var subscriber = await _subcribers.Find(x => x.Id == subscriberId).FirstOrDefaultAsync();

                if (subscriber == null)
                    return Response<Subscriber>.Fail("Category not found", 404);

                var subscriberDTO = new Subscriber
                {
                    Id = subscriber.Id,
                    Email = subscriber.Email,
                    CreateDate = subscriber.CreateDate,
                    UpdateDate = subscriber.UpdateDate
                };
                return Response<Subscriber>.Success(subscriber, 200);
            }
            catch (Exception ex)
            {
                return Response<Subscriber>.Fail(ex.Message, 500);
            }
        }
    }
}
