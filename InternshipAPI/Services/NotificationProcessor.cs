using InternshipAPI.Models;

namespace InternshipAPI.Services;

public class NotificationProcessor
{
    public void Process(Notification notification)
    {
        Console.WriteLine("================================");
        Console.WriteLine($"Source : {notification.Source}");
        Console.WriteLine($"Message: {notification.Message}");
        Console.WriteLine($"Created: {notification.CreatedAt}");
        Console.WriteLine("================================");
    }
}