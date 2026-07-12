using InternshipAPI.Models;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace InternshipAPI.Services;

public class RabbitMqConsumer : BackgroundService
{
    private readonly NotificationProcessor _processor;

    public RabbitMqConsumer(NotificationProcessor processor)
    {
        _processor = processor;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "notifications",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);

            var notification = JsonSerializer.Deserialize<Notification>(json);

            if (notification != null)
            {
                _processor.Process(notification);
            }
        };

        channel.BasicConsume(
            queue: "notifications",
            autoAck: true,
            consumer: consumer);

        return Task.CompletedTask;
    }
}