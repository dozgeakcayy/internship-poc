using InternshipAPI.Interfaces;
using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class WebSocketAdapter : ISourceAdapter
{
    public string Name => "WebSocket";

    public event Func<RawMessage, Task>? OnRawMessage;

    public Task ConnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("WebSocket Adapter Connected");

        return Task.CompletedTask;
    }

    public Task DisconnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("WebSocket Adapter Disconnected");

        return Task.CompletedTask;
    }
}