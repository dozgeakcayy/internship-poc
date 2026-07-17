using InternshipAPI.Interfaces;
using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class ConnectorService : IConnector
{
    private readonly List<ISourceAdapter> _adapters = new();

    public event Func<NotificationEnvelope, Task>? OnMessage;

    public void Register(ISourceAdapter adapter)
    {
        Console.WriteLine($"Adapter Registered: {adapter.Name}");

        _adapters.Add(adapter);

        adapter.OnRawMessage += async raw =>
        {
            Console.WriteLine($"[{raw.Adapter}] Raw message received.");

            var envelope = new NotificationEnvelope
            {
                Source = raw.Adapter,
                Message = raw.Payload,
                ReceivedAt = DateTime.UtcNow
            };

            if (OnMessage != null)
            {
                await OnMessage(envelope);
            }
        };
    }

    public void Unregister(ISourceAdapter adapter)
    {
        Console.WriteLine($"Adapter Unregistered: {adapter.Name}");
        _adapters.Remove(adapter);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"Total registered adapters: {_adapters.Count}");
        Console.WriteLine("Starting all registered adapters...");

        foreach (var adapter in _adapters)
        {
            await adapter.ConnectAsync(cancellationToken);
            Console.WriteLine($"{adapter.Name} started.");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Stopping adapters...");

        foreach (var adapter in _adapters)
        {
            await adapter.DisconnectAsync(cancellationToken);
            Console.WriteLine($"{adapter.Name} stopped.");
        }
    }
}