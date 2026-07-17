using InternshipAPI.Interfaces;
using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class WebhookAdapter : ISourceAdapter
{
    public string Name => "Webhook";

    public event Func<RawMessage, Task>? OnRawMessage;

    public Task ConnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Webhook Adapter Connected");
        return Task.CompletedTask;
    }

    public Task DisconnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Webhook Adapter Disconnected");
        return Task.CompletedTask;
    }

    public async Task ReceiveAsync(string message)
    {
        if (OnRawMessage != null)
        {
            await OnRawMessage(new RawMessage
            {
                Adapter = Name,
                Payload = message
            });
        }
    }
}