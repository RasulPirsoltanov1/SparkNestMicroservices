using SparkNest.Services.MailServiceAPI.Models;

namespace SparkNest.Services.MailServiceAPI.Interfaces
{
    public interface ISubscriberService
    {
        Task<bool> AddAsync(Subscriber subscriber);
        Task<List<Subscriber>> GetAll();
    }
}
