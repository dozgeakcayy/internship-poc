using InternshipAPI.Models;

namespace InternshipAPI.Interfaces;

public interface IConnector
{
    event Func<NotificationEnvelope, Task>? OnMessage;

    void Register(ISourceAdapter adapter);

    void Unregister(ISourceAdapter adapter);

    Task StartAsync(CancellationToken cancellationToken);

    Task StopAsync(CancellationToken cancellationToken);
}