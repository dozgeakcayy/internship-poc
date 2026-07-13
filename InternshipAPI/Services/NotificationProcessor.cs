using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class NotificationProcessor
{
    public Task Process(NotificationEnvelope notification)
    {
        Console.WriteLine("================================");
        Console.WriteLine($"Source : {notification.Source}");
        Console.WriteLine($"Message: {notification.Message}");
        Console.WriteLine($"Received: {notification.ReceivedAt}");
        Console.WriteLine("================================");

        return Task.CompletedTask;
    }
}