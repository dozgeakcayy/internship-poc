using InternshipAPI.Interfaces;
using InternshipAPI.Models;
using StackExchange.Redis;

namespace InternshipAPI.Services;

public class RedisAdapter : ISourceAdapter
{
    private ConnectionMultiplexer? _redis;
    private ISubscriber? _subscriber;

    public string Name => "Redis";

    public event Func<RawMessage, Task>? OnRawMessage;

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Connecting to Redis...");

        var options = ConfigurationOptions.Parse("localhost:6379");
        options.AbortOnConnectFail = false;

        _redis = await ConnectionMultiplexer.ConnectAsync(options);

        _subscriber = _redis.GetSubscriber();

        await _subscriber.SubscribeAsync(
            RedisChannel.Literal("notifications"),
            async (channel, message) =>
            {
                Console.WriteLine($"[Redis] Message received: {message}");

                if (OnRawMessage != null)
                {
                    await OnRawMessage(new RawMessage
                    {
                        Adapter = Name,
                        Payload = message!
                    });
                }
            });

        Console.WriteLine("Subscribed to Redis channel: notifications");
        Console.WriteLine("Waiting for Redis messages...");
        Console.WriteLine("Redis Adapter Connected");
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        if (_subscriber != null)
        {
            await _subscriber.UnsubscribeAllAsync();
        }

        if (_redis != null)
        {
            await _redis.CloseAsync();
        }

        Console.WriteLine("Redis Adapter Disconnected");
    }
}