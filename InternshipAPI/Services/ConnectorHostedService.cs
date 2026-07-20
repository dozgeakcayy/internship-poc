using InternshipAPI.Interfaces;
using InternshipAPI.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace InternshipAPI.Services;

public class ConnectorHostedService : BackgroundService
{
    private readonly IConnector _connector;
    private readonly RabbitMqAdapter _rabbitMqAdapter;
    private readonly NotificationProcessor _processor;
    private readonly WebSocketAdapter _webSocketAdapter;
    private readonly WebhookAdapter _webhookAdapter;
    private readonly ConnectorOptions _options;

    public ConnectorHostedService(
        IConnector connector,
        RabbitMqAdapter rabbitMqAdapter,
        NotificationProcessor processor,
        WebSocketAdapter webSocketAdapter,
        WebhookAdapter webhookAdapter,
        IOptions<ConnectorOptions> options)
    {
        _connector = connector;
        _rabbitMqAdapter = rabbitMqAdapter;
        _processor = processor;
        _webSocketAdapter = webSocketAdapter;
        _webhookAdapter = webhookAdapter;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connector.OnMessage += _processor.Process;

        switch (_options.Provider)
        {
            case "RabbitMQ":
                _connector.Register(_rabbitMqAdapter);
                break;

            case "WebSocket":
                _connector.Register(_webSocketAdapter);
                break;

            case "Webhook":
                _connector.Register(_webhookAdapter);
                break;

            default:
                throw new Exception("Unknown connector provider.");
        }

        await _connector.StartAsync(stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connector.StopAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
}