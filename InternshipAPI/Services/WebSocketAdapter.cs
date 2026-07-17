using InternshipAPI.Interfaces;
using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class WebSocketAdapter : ISourceAdapter
{
    public string Name => "WebSocket";

    public event Func<RawMessage, Task>? OnRawMessage;

    private Timer? _timer;

    public Task ConnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("WebSocket Adapter Connected");

        _timer = new Timer(async _ =>
        {
            if (OnRawMessage != null)
            {
                await OnRawMessage(new RawMessage
                {
                    Adapter = Name,
                    Payload = "Simulated WebSocket Message"
                });
            }

        }, null, 5000, 5000);

        return Task.CompletedTask;
    }

    public Task DisconnectAsync(CancellationToken cancellationToken)
    {
        _timer?.Dispose();

        Console.WriteLine("WebSocket Adapter Disconnected");

        return Task.CompletedTask;
    }
}