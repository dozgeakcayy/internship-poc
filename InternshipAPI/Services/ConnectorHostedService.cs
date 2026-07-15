using InternshipAPI.Interfaces;
using Microsoft.Extensions.Hosting;

namespace InternshipAPI.Services;

public class ConnectorHostedService : BackgroundService
{
    private readonly IConnector _connector;
    private readonly RabbitMqAdapter _rabbitMqAdapter;
    private readonly NotificationProcessor _processor;
    private readonly WebSocketAdapter _webSocketAdapter;
    public ConnectorHostedService(
        WebSocketAdapter webSocketAdapter,
        IConnector connector,
        RabbitMqAdapter rabbitMqAdapter,
        NotificationProcessor processor)
    {
        _connector = connector;
        _rabbitMqAdapter = rabbitMqAdapter;
        _processor = processor;
        _webSocketAdapter = webSocketAdapter;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connector.OnMessage += _processor.Process;

        _connector.Register(_rabbitMqAdapter);
        _connector.Register(_webSocketAdapter);
        await _connector.StartAsync(stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connector.StopAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
}