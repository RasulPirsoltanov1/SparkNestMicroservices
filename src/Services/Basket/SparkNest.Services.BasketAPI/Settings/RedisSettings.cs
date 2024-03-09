namespace SparkNest.Services.BasketAPI.Settings
{
    public class RedisSettings : IRedisSettings
    {
        public string? Host { get; set; }
        public int Port { get; set; }
    }

}
