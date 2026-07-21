using InternshipAPI.Interfaces;
using InternshipAPI.Models;
using StackExchange.Redis;

namespace InternshipAPI.Services;

public class RedisAdapter : ISourceAdapter
{
    private ConnectionMultiplexer? _redis;

    public string Name => "Redis";

    public event Func<RawMessage, Task>? OnRawMessage;

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Connecting to Redis...");

        _redis = await ConnectionMultiplexer.ConnectAsync("localhost:6379");

        Console.WriteLine("Redis Adapter Connected");
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken)
    {
        if (_redis != null)
        {
            await _redis.CloseAsync();
        }

        Console.WriteLine("Redis Adapter Disconnected");
    }
}