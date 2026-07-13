using InternshipAPI.Interfaces;
using Microsoft.Extensions.Hosting;

namespace InternshipAPI.Services;

public class ConnectorHostedService : BackgroundService
{
    private readonly IConnector _connector;
    private readonly RabbitMqAdapter _rabbitMqAdapter;

    public ConnectorHostedService(
        IConnector connector,
        RabbitMqAdapter rabbitMqAdapter)
    {
        _connector = connector;
        _rabbitMqAdapter = rabbitMqAdapter;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _connector.Register(_rabbitMqAdapter);

        await _connector.StartAsync(stoppingToken);

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connector.StopAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }
}