using RabbitMQ.Client;
using System.Text;

namespace InternshipAPI.Services;

public class RabbitMqService
{
    public void SendMessage(string message)
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

        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(
            exchange: "",
            routingKey: "notifications",
            basicProperties: null,
            body: body);

        Console.WriteLine($"Message sent: {message}");
    }
}