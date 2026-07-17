using InternshipAPI.Interfaces;
using Microsoft.Extensions.Hosting;

namespace InternshipAPI.Services;

public class ConnectorHostedService : BackgroundService
{
    private readonly IConnector _connector;
    private readonly RabbitMqAdapter _rabbitMqAdapter;
    private readonly WebSocketAdapter _webSocketAdapter;
    private readonly WebhookAdapter _webhookAdapter;
    private readonly NotificationProcessor _processor;

    public ConnectorHostedService(
        IConnector connector,
        RabbitMqAdapter rabbitMqAdapter,
        WebSocketAdapter webSocketAdapter,
        WebhookAdapter webhookAdapter,
        NotificationProcessor processor)
    {
        _connector = connector;
        _rabbitMqAdapter = rabbitMqAdapter;
        _webSocketAdapter = webSocketAdapter;
        _webhookAdapter = webhookAdapter;
        _processor = processor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connector.OnMessage += _processor.Process;

        _connector.Register(_rabbitMqAdapter);
        _connector.Register(_webSocketAdapter);
        _connector.Register(_webhookAdapter);

        await _connector.StartAsync(stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connector.StopAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
}