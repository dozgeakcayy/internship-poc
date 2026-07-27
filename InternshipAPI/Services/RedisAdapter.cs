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

        var options = new ConfigurationOptions
        {
            EndPoints = { "localhost:6379" },
            AbortOnConnectFail = false,
            ConnectRetry = 5,
            ConnectTimeout = 5000,
            SyncTimeout = 5000,
            ReconnectRetryPolicy = new ExponentialRetry(5000)
        };

        _redis = await ConnectionMultiplexer.ConnectAsync(options);

        _redis.ConnectionFailed += (_, e) =>
        {
            Console.WriteLine($"[Redis] Connection failed: {e.Exception?.Message}");
        };

        _redis.ConnectionRestored += (_, e) =>
        {
            Console.WriteLine("[Redis] Connection restored.");
        };

        _redis.ErrorMessage += (_, e) =>
        {
            Console.WriteLine($"[Redis] Error: {e.Message}");
        };

        _redis.InternalError += (_, e) =>
        {
            Console.WriteLine($"[Redis] Internal error: {e.Exception.Message}");
        };

        Console.WriteLine($"[{DateTime.Now}] Redis connection established.");

        _subscriber = _redis.GetSubscriber();

        await _subscriber.SubscribeAsync(
            RedisChannel.Literal("notifications"),
            async (channel, message) =>
            {
                try
                {
                    Console.WriteLine($"[Redis] Message received: {message}");

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine("[Redis] Empty message ignored.");
                        return;
                    }

                    if (OnRawMessage != null)
                    {
                        Console.WriteLine("[Redis] Raw message received.");

                        await OnRawMessage(new RawMessage
                        {
                            Adapter = Name,
                            Payload = message.ToString()
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Redis] Error while processing message: {ex.Message}");
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
            await _redis.DisposeAsync();
        }

        Console.WriteLine("Redis Adapter Disconnected");
    }
}