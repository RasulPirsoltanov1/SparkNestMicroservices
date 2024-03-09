namespace SparkNest.Services.BasketAPI.Settings
{
    public interface IRedisSettings
    {
        string? Host { get; set; }
        int Port { get; set; }
    }
}