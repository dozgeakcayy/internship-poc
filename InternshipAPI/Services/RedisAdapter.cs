using InternshipAPI.Interfaces;
using InternshipAPI.Models;
using StackExchange.Redis;

namespace InternshipAPI.Services;

public class RedisAdapter : ISourceAdapter
{
    private ConnectionMultiplexer? _redis;
    private ISubscriber? _subscriber;
    private readonly HealthCheckService _health;

    public string Name => "Redis";

    public event Func<RawMessage, Task>? OnRawMessage;

    public RedisAdapter(HealthCheckService health)
{
    _health = health;
}

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Connecting to Redis...");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                _redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");

                Console.WriteLine($"[{DateTime.Now}] Redis connection established.");
                _health.RedisConnected = true;
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
                            Console.WriteLine($"[Redis] Processing Error: {ex.Message}");
                        }
                    });

                Console.WriteLine("Subscribed to Redis channel: notifications");
                Console.WriteLine("Waiting for Redis messages...");
                Console.WriteLine("Redis Adapter Connected");

                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Redis connection failed: {ex.Message}");
                Console.WriteLine("Retrying in 5 seconds...");

                await Task.Delay(5000, cancellationToken);
            }
        }
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping Redis Adapter...");
        _health.RedisConnected = false;
        Console.WriteLine("Waiting for pending operations...");

        if (_subscriber != null)
        {
            await _subscriber.UnsubscribeAllAsync();
        }

        if (_redis != null)
        {
            await _redis.CloseAsync();
        }

        Console.WriteLine("Redis Adapter Disconnected");
        Console.WriteLine("Shutdown completed.");
        Console.WriteLine($"[{DateTime.Now}] Redis resources released.");
    }
}