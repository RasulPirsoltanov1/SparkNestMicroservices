using MongoDB.Bson;
using MongoDB.Driver;
using SparkNest.Services.MailServiceAPI.Interfaces;
using SparkNest.Services.MailServiceAPI.Models;

namespace SparkNest.Services.MailServiceAPI.Services
{
    public class SubscriberService : ISubscriberService
    {
        IMongoDatabase _database;
        IMongoCollection<Subscriber> _subscribers;
        public SubscriberService(IMongoDatabase database)
        {
            _database = database;
            _subscribers = _database.GetCollection<Subscriber>(nameof(Subscriber));
        }

        public async Task<bool> AddAsync(Subscriber subscriber)
        {
            try
            {
                await _subscribers.InsertOneAsync(subscriber);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Subscriber>> GetAll()
        {
            try
            {
                var subscribers = await _subscribers.Find(new BsonDocument()).ToListAsync();
                return subscribers;
            }
            catch (Exception ex)
            {
                return new List<Subscriber>();
            }
        }
    }
}
