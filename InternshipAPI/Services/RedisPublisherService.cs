using StackExchange.Redis;

namespace InternshipAPI.Services;

public class RedisPublisherService
{
    private readonly ConnectionMultiplexer _redis;
    private readonly ISubscriber _subscriber;

    public RedisPublisherService()
    {
        _redis = ConnectionMultiplexer.Connect("localhost:6379");
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