using InternshipAPI.Interfaces;
using InternshipAPI.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace InternshipAPI.Services;

public class RabbitMqAdapter : ISourceAdapter
{
    private IConnection? _connection;
    private IModel? _channel;

    public string Name => "RabbitMQ";

    public event Func<RawMessage, Task>? OnRawMessage;

    public Task ConnectAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: "notifications",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var rawMessage = new RawMessage
            {
                Adapter = Name,
                Payload = message
            };

            if (OnRawMessage != null)
            {
                await OnRawMessage(rawMessage);
            }
        };

        _channel.BasicConsume(
            queue: "notifications",
            autoAck: true,
            consumer: consumer);

        Console.WriteLine("RabbitMQ Adapter Connected");

        return Task.CompletedTask;
    }

    public Task DisconnectAsync(CancellationToken cancellationToken)
    {
        _channel?.Close();
        _connection?.Close();

        Console.WriteLine("RabbitMQ Adapter Disconnected");

        return Task.CompletedTask;
    }
}