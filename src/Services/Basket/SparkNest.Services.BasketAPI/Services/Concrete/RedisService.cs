using StackExchange.Redis;

namespace SparkNest.Services.BasketAPI.Services.Concrete
{
    public class RedisService
    {
        string _host;
        int _port;
        ConnectionMultiplexer _connectionMultiplexer;

        public RedisService(string host, int port)
        {
            _host = host;
            _port = port;
        }
        public void Connect() => _connectionMultiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase GetDb(int db = 1)=>_connectionMultiplexer.GetDatabase(db);
    }
}
