using InternshipAPI.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace InternshipAPI.Services;

public class RabbitMqService
{
    public void Publish(Notification notification)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "notifications",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var json = JsonSerializer.Serialize(notification);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish(
            exchange: "",
            routingKey: "notifications",
            body: body);

        Console.WriteLine($"Published: {json}");
    }
}