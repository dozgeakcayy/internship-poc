using InternshipAPI.Interfaces;
using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class FakeAdapter : ISourceAdapter
{
    public string Name => "Fake";

    public event Func<RawMessage, Task>? OnRawMessage;

    public async Task ConnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Fake Adapter Connected");

        await Task.Delay(3000, cancellationToken);

        if (OnRawMessage != null)
        {
            await OnRawMessage.Invoke(new RawMessage
            {
                Adapter = Name,
                Payload = "Hello from Fake Adapter!"
            });
        }
    }

    public Task DisconnectAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Fake Adapter Disconnected");
        return Task.CompletedTask;
    }
}