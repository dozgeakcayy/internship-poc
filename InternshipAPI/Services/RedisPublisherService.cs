using StackExchange.Redis;

namespace InternshipAPI.Services;

public class RedisPublisherService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly ISubscriber _subscriber;

    public RedisPublisherService()
    {
        var options = new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" },
            AbortOnConnectFail = false,
            ConnectRetry = 5,
            ConnectTimeout = 5000,
            SyncTimeout = 5000,
            ReconnectRetryPolicy = new ExponentialRetry(5000)
        };

        _redis = ConnectionMultiplexer.Connect(options);

        _redis.ConnectionFailed += (_, e) =>
        {
            Console.WriteLine($"[Publisher] Redis connection failed: {e.Exception?.Message}");
        };

        _redis.ConnectionRestored += (_, e) =>
        {
            Console.WriteLine("[Publisher] Redis connection restored.");
        };

        _subscriber = _redis.GetSubscriber();
    }

    public async Task PublishAsync(string message)
    {
        await _subscriber.PublishAsync(
            RedisChannel.Literal("notifications"),
            message);

        Console.WriteLine($"Published to Redis: {message}");
    }
}