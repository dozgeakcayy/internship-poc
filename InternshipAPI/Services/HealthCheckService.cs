using RabbitMQ.Client;
using StackExchange.Redis;

namespace InternshipAPI.Services;

public class HealthCheckService
{
    public async Task<bool> CheckRedisAsync()
    {
        try
        {
            var redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");
            return redis.IsConnected;
        }
        catch
        {
            return false;
        }
    }

    public bool CheckRabbitMq()
    {
        try
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using var connection = factory.CreateConnection();
            return connection.IsOpen;
        }
        catch
        {
            return false;
        }
    }
}